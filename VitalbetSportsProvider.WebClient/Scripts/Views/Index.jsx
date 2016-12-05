class Sport extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isOpen: false,
            events: [],
            matches: []
        };
    }

    onSportClick() {
        this.state.isOpen = !this.state.isOpen;
        if (this.state.isOpen) {
            app.hub.call({
                hub: "sportsHub",
                method: "getEvents",
                params: [this.props.sport.Id],
                onDone: this.onEventsLoaded.bind(this)
            });
        } else {
            this.setState({ events: [] });
        }
    }

    onEventClick(eventId) {
        app.hub.call({
            hub: "sportsHub",
            method: "getMatches",
            params: [eventId],
            onDone: this.onMatchesLoaded.bind(this)
        });
    }

    onEventsLoaded(result) {
        this.setState({ events: result, isOpen: this.state.isOpen });
    }

    onMatchesLoaded(result) {
        this.setState({ matches: result });
    }

    render() {

        const eventsSort =
            firstBy((a, b) => b.MatchesCount - a.MatchesCount)
            .thenBy((a, b) => a.Name.localeCompare(b.Name));

        const matchesSort = (a, b) => a.Name.localeCompare(b.Name);

        const onEventClick = this.onEventClick.bind(this);

        return (
            <div className="row">
                <div className="col-md-3">
                    <h4>
                        <span className={"glyphicon glyphicon-chevron-{0}".format(this.state.isOpen ? "down" : "right") }></span> 
                        <span onClick={this.onSportClick.bind(this)}> {this.props.sport.Name} </span> 
                        <span className="badge">{this.props.sport.EventsCount}</span>
                    </h4>
                    {
                    this.state.events.sort(eventsSort).map(function (ev) {
                        return (
                            <div>
                                <span onClick={onEventClick.bind(null, ev.Id)}>{ev.Name}</span> 
                                <span className="badge">{ev.MatchesCount}</span>
                            </div>
                        );
                    })
                }
                </div>
                <div className="col-md-9">
                {
                    this.state.matches.sort(matchesSort).map(function (ev) {
                        return (
                            <div className="col-md-4">
                                <span>{ev.Name}</span> 
                            </div>
                        );
                    })
                }
                </div>
            </div>
        );
    }
};

class Index extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            sports: []
        };
    }

    onSportsLoaded(result)
    {
        this.setState({ sports: result });
    }

    render() {

        const sportsSort =
            firstBy((a, b) => b.EventsCount - a.EventsCount)
            .thenBy((a, b) => a.Name.localeCompare(b.Name));

        return (
            <div>
                {
                    this.state.sports.sort(sportsSort).map(function (sport) {
                        return <Sport sport={sport} />;
                    })
                }
            </div>
        );
    }

    componentDidMount() {
        app.hub.call({
            hub: "sportsHub",
            method: "getSports",
            onDone: this.onSportsLoaded.bind(this)
        });
    }
};

ReactDOM.render(React.createElement(Index, null), document.getElementById("react-index"));
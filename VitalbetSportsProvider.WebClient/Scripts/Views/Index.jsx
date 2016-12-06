class Bets extends React.Component {
    render() {

        const betsSort = (a, b) => a.Name.localeCompare(b.Name);
        
        return (
            <div className="col-md-9">
                {
                this.props.bets.sort(betsSort).map(function (bet) {
                    return (
                        <div key={"Bet_{0}".format(bet.Id)} className="col-md-12">
                            <h4>{bet.Name}</h4> 
                            <div>
                                {
                                    bet.Odds.map(function (odds) {
                                        return <div key={"Odds_{0}".format(odds.Id)} className="col-md-4"><strong>{odds.Name}</strong> {odds.Value}</div>;
                                    })
                                }
                            </div>
                        </div>
                    );
                })
            }
            </div>
        );
    }
};

class Matches extends React.Component {
    render() {

        const matchesSort = (a, b) => a.Name.localeCompare(b.Name);

        const onMatchClick = this.props.onMatchClick;

        return (
            <div className="col-md-9">
            {
                this.props.matches.sort(matchesSort).map(function (match) {
                    return (
                        <div key={"Match_{0}".format(match.Id)} className="col-md-4">
                            <span onClick={onMatchClick.bind(null, match.Id) }>{match.Name}</span> 
                        </div>
                    );
                })
            }
            </div>
        );
    }
};

class Events extends React.Component {
    render() {

        const eventsSort =
            firstBy((a, b) => b.MatchesCount - a.MatchesCount)
            .thenBy((a, b) => a.Name.localeCompare(b.Name));

        const onEventClick = this.props.onEventClick;

        return (
            <div className="col-md-12">
                {
                    this.props.events.sort(eventsSort).map(function (ev) {
                        return (
                            <div key={"Event_{0}".format(ev.Id)}>
                                <span onClick={onEventClick.bind(null, ev.Id)}>{ev.Name}</span> 
                                <span className="badge">{ev.MatchesCount}</span>
                            </div>
                        );
                    })
                }
            </div>
        );
    }
};

class Sport extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isOpen: false,
            events: [],
            matches: [],
            bets: []
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

    onMatchClick(matchId) {
        app.hub.call({
            hub: "sportsHub",
            method: "getBets",
            params: [matchId],
            onDone: this.onBetsLoaded.bind(this)
        });
    }

    onEventsLoaded(result) {
        this.setState({ events: result, isOpen: this.state.isOpen });
    }

    onMatchesLoaded(result) {
        this.setState({ bets: [], matches: result });
    }

    onBetsLoaded(result) {
        this.setState({ bets: result, matches: [] });
    }

    render() {

        return (
            <div className="row">
                <div className="col-md-3">
                    <h4>
                        <span className={"glyphicon glyphicon-chevron-{0}".format(this.state.isOpen ? "down" : "right") }></span> 
                        <span onClick={this.onSportClick.bind(this)}> {this.props.sport.Name} </span> 
                        <span className="badge">{this.props.sport.EventsCount}</span>
                    </h4>
                    <Events events={this.state.events} onEventClick={this.onEventClick.bind(this)} />
                </div>
                <Matches matches={this.state.matches} onMatchClick={this.onMatchClick.bind(this)} />
                <Bets bets={this.state.bets} />
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
                        return <Sport key={"Sport_{0}".format(sport.Id)} sport={sport} />;
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
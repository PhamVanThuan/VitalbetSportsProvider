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

    render () {
        return (
            <div>
                {
                    this.state.sports.map(function (sport) {
                        return (
                            <div className="row">
                                <div className="col-md-2"><h4>{sport.Name}</h4></div>
                                <div className="col-md-4">
                                    {
                                        sport.Events.map(function (ev) {
                                            return (
                                                <div>{ev.Name}</div>
                                            );
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

    componentDidMount() {
        app.hub.call({
            hub: "sportsHub",
            method: "getSports",
            onDone: this.onSportsLoaded.bind(this)
        });
    }
};

ReactDOM.render(React.createElement(Index, null), document.getElementById("react-index"));
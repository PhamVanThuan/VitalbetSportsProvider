var Home = React.createClass({
    render: function() {
        return (
            <div className="container">
	            <div className="navbar-header">
		            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
		            <span className="icon-bar" />
		            <span className="icon-bar" />
		            <span className="icon-bar" />
		            </button>
		            <a className="navbar-brand" href="/">Vitalbet Feed</a>
	            </div>
            </div>
        );
    }
});

ReactDOM.render(React.createElement(Home, null), document.getElementById("react-navigation"));
class Navigation extends React.Component {
    render () {
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
};

ReactDOM.render(React.createElement(Navigation, null), document.getElementById("react-navigation"));
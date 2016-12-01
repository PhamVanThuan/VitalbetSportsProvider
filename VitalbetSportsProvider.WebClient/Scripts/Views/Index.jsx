var Home = React.createClass({
    render: function() {
        return (
            <div className="row">
	            <div className="col-md-4">
		            <h2>Your information</h2>
		            <p>This section shows how you can call ASP.NET Web API to get the user details.</p>
		            <p data-bind="text: myHometown" />
		            <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=273732">Learn more »</a></p>
	            </div>
	            <div className="col-md-4">
		            <h2>Getting started</h2>
		            <p>
		                ASP.NET Single Page Application (SPA) helps you build applications that include significant client-side interactions using HTML, CSS, and JavaScript.
		                It's now easier than ever before to getting started writing highly interactive web applications.
		            </p>
		            <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=273732">Learn more »</a></p>
	            </div>
	            <div className="col-md-4">
		            <h2>Web Hosting</h2>
		            <p>You can easily find a web hosting company that offers the right mix of features and price for your applications.</p>
		            <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301867">Learn more »</a></p>
	            </div>
            </div>
        );
    }
});

ReactDOM.render(React.createElement(Home, null), document.getElementById("react-index"));
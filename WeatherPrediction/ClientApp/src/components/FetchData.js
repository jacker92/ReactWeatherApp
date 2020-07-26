import React, { Component } from 'react';
import 'react-vis/dist/style.css';
import Chart from './Chart.js';
import Table from './Table.js';

export class FetchData extends Component {

    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { searchBoxValue: '', data: [], searchInvoked: false };

        this.handleChange = this.handleChange.bind(this);
        this.invokeSearch = this.invokeSearch.bind(this);
        this.handleKeyDown = this.handleKeyDown.bind(this);
    }

    componentDidMount() {
    }

    handleChange(event) {
        this.setState({ searchBoxValue: event.target.value });
    }

    invokeSearch() {
        this.populateWeatherData(this.state.searchBoxValue);
    }

    handleKeyDown(e) {
        if (e.key === 'Enter') {
            this.invokeSearch();
        }
    }

    render() {
        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                Enter search terms: <input type="text" onKeyDown={this.handleKeyDown} onChange={this.handleChange} autoFocus></input><button id="searchButton" className="btn btn-primary" onClick={this.invokeSearch}>Search</button>
                <div id="xyplot">
                    <Chart forecasts={this.state.data} searchInvoked={this.state.searchInvoked} />
                </div>
                <Table data={this.state.data}/>
            </div>
        );
    }

    async populateWeatherData(searchString) {
        if (!searchString || this.state.searchInvoked) {
            return;
        }

        document.getElementById("xyplot").style.display = "block";

        this.setState({ searchInvoked: true });

        document.getElementById("searchButton").disabled = true;

        const response = await fetch('weatherforecast', {
            body: JSON.stringify({ "searchString": searchString }),
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        const responseData = await response.json();

        if (responseData.length == 0) {
            document.getElementById("xyplot").style.display = "none";
        } else {
            document.getElementById("xyplot").style.display = "block";
        }

        this.setState({ data: responseData });

        document.getElementById("searchButton").disabled = false;

        setTimeout(() => {
            this.setState({ searchInvoked: false });
        }, 500)
    }
}

export default FetchData;

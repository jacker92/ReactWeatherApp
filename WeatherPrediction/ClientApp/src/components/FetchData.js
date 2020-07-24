import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], searchBoxValue: '' };

        this.handleChange = this.handleChange.bind(this);
        this.invokeSearch = this.invokeSearch.bind(this);
    }

    componentDidMount() {
    }

    handleChange(event) {
        this.setState({ searchBoxValue: event.target.value });
    }

    invokeSearch() {
        document.getElementById("searchButton").disabled = true;

        this.populateWeatherData(this.state.searchBoxValue);
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Date</th>
                        <th>Temperature (C)</th>
                        <th>Minimum Temperature</th>
                        <th>Maximum Temperature</th>
                        <th>Country</th>
                        <th>City</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.id}</td>
                            <td>{forecast.formattedDate}</td>
                            <td>{forecast.temperature.toFixed(2)}</td>
                            <td>{forecast.minimumTemperature.toFixed(2)}</td>
                            <td>{forecast.maximumTemperature.toFixed(2)}</td>
                            <td>{forecast.country}</td>
                            <td>{forecast.city}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                Enter search terms: <input type="text" onChange={this.handleChange}></input><button id="searchButton" className="btn btn-primary" onClick={this.invokeSearch}>Search</button>
                {contents}
            </div>
        );
    }

    async populateWeatherData(searchString) {
        if (!searchString) {
            return;
        }

        const response = await fetch('weatherforecast', {
            body: JSON.stringify({ "searchString": searchString }),
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
        });
        const data = await response.json();
        this.setState({ forecasts: data });
        document.getElementById("searchButton").disabled = false;
    }
}

import React, { Component } from 'react';
import { FlexibleWidthXYPlot, LineSeries, XAxis, YAxis, HorizontalGridLines, VerticalGridLines } from 'react-vis';
import 'react-vis/dist/style.css';
import ReactLoading from 'react-loading';

export class FetchData extends Component {

    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], searchBoxValue: '', data: [], searchInvoked: false };

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
                        <th>Sunrise</th>
                        <th>Sunset</th>
                        <th>Country</th>
                        <th>City</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.slice(0, 1).map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.id}</td>
                            <td>{forecast.formattedDate}</td>
                            <td>{forecast.temperature.toFixed(2)}</td>
                            <td>{forecast.minimumTemperature.toFixed(2)}</td>
                            <td>{forecast.maximumTemperature.toFixed(2)}</td>
                            <td>{forecast.formattedSunrise}</td>
                            <td>{forecast.formattedSunset}</td>
                            <td>{forecast.country}</td>
                            <td>{forecast.city}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        const axisStyle = {
            ticks: {
                fontSize: '14px',
                color: '#121212'
            },
            title: {
                fontSize: '16px',
                color: '#121212'
            }
        }

        let chart;

        if (this.state.searchInvoked) {
            chart = <ReactLoading id="loadingSpoke" type="spokes" color="red" height={500} width={500} />
        } else {
            chart = <FlexibleWidthXYPlot height={500}>
                <VerticalGridLines style={{ stroke: '#B7E9ED' }} />
                <HorizontalGridLines style={{ stroke: '#B7E9ED' }} />
                <LineSeries opacity={1} stroke={'#121212'} data={this.state.forecasts.map((obj, index) => { return { x: obj.dateInUnixTime, y: obj.temperature } })} />
                <XAxis
                    xType="time"
                    tickFormat={(d) => {
                        const date = new Date(d)
                        return date.toLocaleString("fi-FI");
                    }}
                    hideLine
                    style={axisStyle}
                    tickTotal={5}
                />
                <YAxis />
            </FlexibleWidthXYPlot>
        }

        let contents = FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                Enter search terms: <input type="text" onKeyDown={this.handleKeyDown} onChange={this.handleChange} autoFocus></input><button id="searchButton" className="btn btn-primary" onClick={this.invokeSearch}>Search</button>
                <div id="xyplot">
                    {chart}
                </div>
                {contents}
            </div>
        );
    }

    async populateWeatherData(searchString) {
        if (!searchString || this.state.searchInvoked) {
            return;
        }

        this.setState({ searchInvoked: true });

        document.getElementById("searchButton").disabled = true;

        const response = await fetch('weatherforecast', {
            body: JSON.stringify({ "searchString": searchString }),
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        const data = await response.json();

        if (data.length == 0) {
            document.getElementById("xyplot").style.display = "none";
        } else {
            document.getElementById("xyplot").style.display = "block";
        }

        this.setState({ forecasts: data });

        document.getElementById("searchButton").disabled = false;

        setTimeout(() => {
            this.setState({ searchInvoked: false });
        }, 500)
    }
}

export default FetchData;

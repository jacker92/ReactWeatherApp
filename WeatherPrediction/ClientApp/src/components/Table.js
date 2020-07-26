import React, { Component } from 'react';

export class Table extends Component {

    constructor(props) {
        super(props);
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

        let contents = Table.renderForecastsTable(this.props.data);

        return (
            <div>{contents}</div>
        );
    }

}

export default Table;
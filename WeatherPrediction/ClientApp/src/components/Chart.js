import React, { Component } from 'react';
import ReactLoading from 'react-loading';
import { FlexibleWidthXYPlot, LineSeries, XAxis, YAxis, HorizontalGridLines, VerticalGridLines } from 'react-vis';

export class Chart extends Component {

    constructor(props) {
        super(props);
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

        if (this.props.searchInvoked) {
            chart = <ReactLoading id="loadingSpoke" type="spokes" color="red" height={'10%'} width={'10%'} />
        } else {
            chart = <FlexibleWidthXYPlot height={this.props.height/2}>
                <VerticalGridLines style={{ stroke: '#B7E9ED' }} />
                <HorizontalGridLines style={{ stroke: '#B7E9ED' }} />
                <LineSeries opacity={1} stroke={'#121212'} data={this.props.forecasts.map((obj, index) => { return { x: obj.dateInUnixTime, y: obj.temperature } })} />
                <XAxis
                    xType="time"
                    tickFormat={(d) => {
                        const date = new Date(d)
                        let dateString = date.toLocaleString("fi-FI");
                        return this.props.width > 750 ? dateString : dateString.substring(0, 8);
                    }}
                    hideLine
                    style={axisStyle}
                    tickTotal={5}
                />
                <YAxis />
            </FlexibleWidthXYPlot>
        }

        return (
            <div>{chart}</div>
            );
    }
}

export default Chart;


import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import TripCard from './TripCard'

class TripsPage extends React.Component {
    constructor(props){
        super(props)

        this.state = {
            success: false
        }
    }

    componentWillMount() {
        this.props.getAllTrips();
    }

    render() {
        if (this.props.trips) {
            return (
                <div>
                    <div>
                        {this.props.trips.map(trip =><TripCard key={trip.tripId} trip={trip} />)}
                    </div>
                </div>
            );
        }
        else {
            return (
                <div>
                    loading
                </div>
            )
        }
    }
}
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        trips: state.trips
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(TripsPage));

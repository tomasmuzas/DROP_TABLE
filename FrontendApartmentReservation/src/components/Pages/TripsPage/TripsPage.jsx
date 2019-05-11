import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import TripCard from './TripCard'

class TripsPage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trips: [{ tripId: "f55fb047-3865-4e5d-a9ab-0b1f15db2975", DestinationOffice: "Didlaukis", DepartureDate: "2019-04-23", ReturnDate:"2019-05-24" }]
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
                        {this.props.trips.map(trip => <TripCard trip={trip} />)}
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

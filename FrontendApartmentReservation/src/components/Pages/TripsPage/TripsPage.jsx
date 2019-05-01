import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import TripCard from './TripCard'

class TripsPage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            trips: [{ tripId: 1, DestinationOffice: "Didlaukis", DepartureDate: "2019-04-23", ReturnDate:"2019-05-24" }]
        }
    }


    componentWillMount() {
    }

    render() {
        if (this.state.trips) {
            return (
                <div>
                    <div>
                        {this.state.trips.map(trip => <TripCard trip={trip} />)}
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

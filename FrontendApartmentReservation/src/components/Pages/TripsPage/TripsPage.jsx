import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class TripsPage extends React.Component {

    componentWillMount() {
        this.props.getAllTrips();
    }

    render() {
        if (this.props.trips) {
            return (
                <div>
                     <div>
                         This is TripsPage
                        {this.props.trips.map(trip => <div> {trip.destinationOffice} </div>)}
                    </div>
                </div>
            );
        }
        else{
            return(
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

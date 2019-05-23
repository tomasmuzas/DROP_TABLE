import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import TripCard from './TripCard'
import { withTranslation } from "react-i18next";
import { GridLoader } from 'react-spinners';

class TripsPage extends React.Component {
    componentWillMount() {
        this.props.getAllTrips();
        this.props.clearMergableTrips();
    }

    render() {
        const { t } = this.props;
        if (this.props.trips.length >= 1) {
            return (
                <div>
                    <h1 className="row justify-content-md-center mt-5">{t("MyOrganizedTrips")}</h1>
                    <div>
                        {this.props.trips.map(trip => <TripCard key={trip.tripId} trip={trip} mergeable={false} />)}
                    </div>
                </div>
            );
        }
        else {
            return (
                <div className='center-div'>
                    <GridLoader
                        sizeUnit={"px"}
                        size={50}
                        color={'red'}
                    />
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

export default (connect(mapStateToProps, mapDispatchToProps)(withTranslation()(TripsPage)));

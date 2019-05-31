import React from 'react';
import i18next from 'i18next';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import TripCard from './TripCard';
import { GridLoader } from 'react-spinners';


class MergeTrips extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentTrip: []
        };

    }

    componentWillMount() {
        this.props.getMergeableTrips(this.props.match.params.tripId);
        this.setState({
            loading: true
        })
    }

    componentWillReceiveProps(newProps) {
        this.setState({
            loading: false
        })
    }

    getCurrentTrip(trips) {
        if (trips) {
            return trips.find(trip => trip.tripId === this.props.match.params.tripId);
        }
        else {
            this.props.getAllTrips();
        }
    }

    getTripsList() {
        if ((this.props.mergeableTrips === [] || this.props.mergeableTrips.length === 0) && this.state.loading) {
            return (<div className="row mt-5">
                <div className="col-12">
                    <h1>{i18next.t('NoTripsToMerge')}</h1>
                </div>
            </div>
            )
        }
        if (this.props.mergeableTrips.length > 0) {
            return (
                <div className="pl-5">
                    <h3> {i18next.t("TripsPossibleToMerge")}</h3>
                    {this.props.mergeableTrips.map(trip => <TripCard key={trip.tripId} trip={trip} mergeable={true}
                        mergeableTripId={this.props.match.params.tripId} />)}
                </div>
            )
        }
        else {
            return (
                <div className="center-outer-div">
                    <div className='center-div'>
                        <GridLoader
                            sizeUnit={"px"}
                            size={50}
                            color={'red'}
                        />
                    </div>
                </div>
            )
        }
    }

    render() {
        const { t } = this.props;
        var currentTrip = this.getCurrentTrip(this.props.trips);
        if (currentTrip) {
            var tripStartTime = new Date(currentTrip.startTime);
            var tripEndTime = new Date(currentTrip.endTime);
            return (
                <div>
                    <div className="pl-5 pt-3">
                        <h3>{t("MyTripToMerge")}</h3>
                        <h5> {t("DestinationOffice")} : {currentTrip.office.address} </h5>
                        <h5> {t("DepartureDate")} : {tripStartTime.toLocaleDateString('lt-LT')} ,
                         {t("ReturnDate")} : {tripEndTime.toLocaleDateString('lt-LT')} </h5>
                    </div>
                    {this.getTripsList()}
                </div>
            );
        }
        else {
            return (
                <div className="center-outer-div">
                    <div className='center-div'>
                        <GridLoader
                            sizeUnit={"px"}
                            size={50}
                            color={'red'}
                        />
                    </div>
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
        trips: state.trips,
        mergeableTrips: state.mergeableTrips
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(MergeTrips));
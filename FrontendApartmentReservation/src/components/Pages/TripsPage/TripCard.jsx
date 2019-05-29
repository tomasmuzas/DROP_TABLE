import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import { Link } from 'react-router-dom';
import i18n from "../../../i18n";
import Button from '@material-ui/core/Button';

class TripCard extends React.Component {
    constructor(props) {
        super(props);
        this.state ={
            showDetails: true
        }

        this.mergeTrip = this.mergeTrip.bind(this);
        this.deleteTrip = this.deleteTrip.bind(this);
    }

    mergeTrip() {
        this.props.mergeTrips(this.props.trip.tripId, this.props.mergeableTripId);
    }

    deleteTrip(){
        var that = this;
        var promise = that.props.deleteTrip(that.props.trip.tripId).then(function (result) {
            if (result === 200) {
                that.props.updateState();
            }
        });
    }

    render() {
        const { t, trip, mergeable } = this.props;
        var tripStartTime = new Date(trip.startTime);
        var tripEndTime = new Date(trip.endTime);
        return (
          <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius:'5pt' }}>
                <div className="col-lg-6 col-12 justify-content-md-center pt-3 pb-3">
                    <h4><b>{t("TripDestination")} {trip.office.address}</b></h4>
                    <h6 >{t("DepartureDate")}:  {tripStartTime.toLocaleDateString('lt-LT')}</h6>
                    <h6 >{t("ReturnDate")}:  {tripEndTime.toLocaleDateString('lt-LT')}</h6>
                </div>
                <div className="col-lg-2 col-12 pt-5" hidden={mergeable}>
                    <Link to={'/trip/' + trip.tripId + '/merge'} style={{ textDecoration: 'none', color: 'black' }}>
                        <Button className="justify-content-center" variant="outlined" color="secondary">
                            {t("Merge")}
                        </Button>
                    </Link>
                </div>
                <div className="col-lg-2 col-12 pt-5" hidden={mergeable}>
                    <Link to={'/trip/' + trip.tripId} style={{ textDecoration: 'none', color: 'black' }}>
                        <Button variant="outlined" color="primary" >
                            {t("TripInformation")}
                        </Button>
                    </Link>
                </div>
                <div className="col-lg-2 col-12 pt-5 pb-5" hidden={mergeable}>
                        <Button variant="outlined" color="secondary" onClick={this.deleteTrip}>
                            {t("DeleteTrip")}
                        </Button>
                </div>
                <div className="col-lg-6 col-12 pt-5 pl-5" hidden={!mergeable}>
                    <Button className="mx-auto" variant="outlined" color="secondary" onClick={this.mergeTrip} >
                        {t("Merge")}
                    </Button>
                </div>
            </div>
        )

    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        trips : state.trips
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(TripCard));
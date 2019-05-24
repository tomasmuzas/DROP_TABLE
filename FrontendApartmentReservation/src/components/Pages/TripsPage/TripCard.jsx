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

        this.mergeTrip = this.mergeTrip.bind(this);
    }

    mergeTrip() {
        this.props.mergeTrips(this.props.trip.tripId, this.props.mergeableTripId);
    }

    render() {
        const { t, trip, mergeable } = this.props;
        var tripStartTime = new Date(trip.startTime);
        var tripEndTime = new Date(trip.endTime);
        return (
          <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                <div className="col-lg-6 col-12 justify-content-md-center pt-3 pb-3 pl-5">
                    <h5 >{t("TripDestination")}: {trip.office.address}</h5>
                    <h6 >{t("DepartureDate")}:  {tripStartTime.toLocaleDateString('lt-LT')}</h6>
                    <h6 >{t("ReturnDate")}:  {tripEndTime.toLocaleDateString('lt-LT')}</h6>
                </div>
                <div className="col-lg-3 col-12 pt-5 pl-5" hidden={mergeable}>
                    <Link to={'/trip/' + trip.tripId + '/merge'} style={{ textDecoration: 'none', color: 'black' }}>
                        <Button className="justify-content-center" variant="outlined" color="secondary">
                            {t("Merge")}
                        </Button>
                    </Link>
                </div>
                <div className="col-lg-3 col-12 pt-5 pl-5" hidden={mergeable}>
                    <Link to={'/trip/' + trip.tripId} style={{ textDecoration: 'none', color: 'black' }}>
                        <Button variant="outlined" color="primary" >
                            {t("TripInformation")}
                        </Button>
                    </Link>
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

    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(TripCard));
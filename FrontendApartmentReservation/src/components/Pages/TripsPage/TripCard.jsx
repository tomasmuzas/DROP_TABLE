import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import i18n from "../../../i18n"


class TripCard extends React.Component {
    render() {
        const { t, trip } = this.props;
        var tripStartTime = new Date(trip.startTime);
        var tripEndTime = new Date(trip.endTime);
        return (

            <Link to={'/trip/' + trip.tripId} style={{ textDecoration: 'none', color: 'black' }}>
                <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                    <div className="col justify-content-md-center pt-3 pb-3 pl-5">
                        <h5>{t("DestinationOffice")}: {trip.destinationOffice}</h5>
                        <h6>{t("DepartureDate")}:  {tripStartTime.toLocaleDateString('lt-LT')}</h6>
                        <h6>{t("ReturnDate")}:  {tripEndTime.toLocaleDateString('lt-LT')}</h6>
                    </div>
                    <div className="col pr-5">
                        <div style={{ float: "right" }}>
                        </div>
                    </div>
                </div>
            </Link>
        )

    }
}

export default withTranslation()(TripCard);
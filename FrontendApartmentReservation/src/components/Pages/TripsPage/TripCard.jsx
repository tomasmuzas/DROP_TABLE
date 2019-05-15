import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import i18n from "../../../i18n"


class TripCard extends React.Component {
    render() {
        const { t, trip } = this.props;
        return (

            <Link to={'/trip/' + trip.tripId} style={{ textDecoration: 'none', color: 'black' }}>
                <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                    <div className="col justify-content-md-center nameDiv pl-5">
                        <h5>{t("DestinationOffice")}: {trip.destinationOffice}</h5>
                        <h6>{t("DepartureDate")}: {trip.startTime} {t("ReturnDate")}: {trip.endTime}</h6>
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
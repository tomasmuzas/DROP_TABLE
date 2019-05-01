import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Fab from '@material-ui/core/Fab';
import Icon from '@material-ui/icons/Edit';
import IconButton from '@material-ui/core/IconButton';
import i18n from "../../../i18n"


function TripCard(props) {
    const { t, trip } = props;
    return (

        <Link to={'/trip/' + trip.tripId}style={{ textDecoration: 'none', color:'black' }}>
            <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                <div className="col justify-content-md-center nameDiv pl-5">
                    <h5>{t("DestinationOffice")}: {trip.DestinationOffice}</h5>
                    <h6>{t("DepartureDate")}: {trip.DepartureDate} {t("ReturnDate")}: {trip.ReturnDate}</h6>
                </div>
                <div className="col pr-5">
                    <div style={{ float: "right" }}>
                    </div>
                </div>
            </div>
        </Link>
    )
}

export default withTranslation()(TripCard);
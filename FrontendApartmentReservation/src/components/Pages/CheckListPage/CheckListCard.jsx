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

function CheckListCard(props) {

    const { checkListInfo, t, tripId } = props;
    return (
        <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
            <div className="col justify-content-md-center nameDiv pl-5">
                <h5>{checkListInfo.employee.firstName} {checkListInfo.employee.lastName}</h5>
            </div>
            <div className="col pr-5">
                <div style={{ float: "right" }}>
                    <FormControlLabel
                        control={<Checkbox checked={checkListInfo.isApartmentRequired} />}
                        label={t("ApartmentRequired")}
                    />
                    <FormControlLabel
                        control={<Checkbox checked={checkListInfo.isFlightRequired} />}
                        label={t("FlightRequired")}
                    />
                    <FormControlLabel
                        control={<Checkbox checked={checkListInfo.isCarRentRequired} />}
                        label={t("CarRequired")}
                    />
                    <Link to={'/' + tripId + '/checklist/' + checkListInfo.employee.id}>
                        <IconButton className="EditIconButton">
                            <Icon>edit_icon</Icon>
                        </IconButton>
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default withTranslation()(CheckListCard);
import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
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
import arrowUp from './up-arrow.png';
import arrowDown from './down-arrow.png';

class CheckListCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showDetails: false,
            carInfo: { IsRequired: true, carNumber: 12342, PickUpPoint: 'Didlaukio g. 59' },
            apartmentsInfo: { IsRequired: true, AppartmentsAddress: 'Didlaukio g. 59' },
        }

        this.handleShowDetailsChange = this.handleShowDetailsChange.bind(this);
    }

    handleShowDetailsChange(e) {
        this.setState({
            showDetails: !this.state.showDetails
        })
    }

    componentWillMount() {
        this.props.getChecklist(this.props.checkListInfo.employee.id, this.props.tripId);
    }

    showFlight(checklist) {
        const { t } = this.props;
        if (checklist) {
            var flightDate = new Date(checklist.flight.flightTime);
            return (
                <div className="col-12 col-lg-4" hidden={!checklist.flight.isRequired}>
                    <h6> {t("FlightNumber")}: {checklist.flight.flightNumber}</h6>
                    <h6> {t("FlightCompany")}:  {checklist.flight.company}</h6>
                    <h6> {t("AirportAddress")}:  {checklist.flight.airportAddress}</h6>
                    <h6> {t("FlightTime")}: {flightDate.toLocaleDateString('lt-LT') + " " + flightDate.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })} </h6>
                </div>
            )
        }
        else {
            return (
                <div>
                    loading
                </div>
            )
        }
    }

    showCar(checklist) {
        const { t } = this.props;
        if (checklist) {
            var rentStartTime = new Date(checklist.car.rentStartTime);
            var rentEndTime = new Date(checklist.car.rentEndTime);
            return (
                <div className="col-12 col-lg-4" hidden={!checklist.car.isRequired}>
                    <h6> {t("CarNumber")}: {checklist.car.carNumber}</h6>
                    <h6> {t("CarAddress")}:  {checklist.car.carAddress}</h6>
                    <h6> {t("RentStartTime")}: {rentStartTime.toLocaleDateString('lt-LT') + " " + rentStartTime.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })}</h6>
                    <h6> {t("RentEndTime")}: {rentEndTime.toLocaleDateString('lt-LT') + " " + rentEndTime.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })}</h6>
                </div>
            )
        }
        else {
            return (
                <div>
                    loading
                </div>
            )
        }
    }

    render() {
        const { checkListInfo, t, tripId } = this.props;
        return (
            <div>
                <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                    <div className="col justify-content-md-center nameDiv pl-5 pb-lg-0 pb-5">
                        <h5>{checkListInfo.employee.firstName} {checkListInfo.employee.lastName}</h5>
                        {checkListInfo.HasAcceptedTripConfirmation ? 
                            <h5 style={{ color: "#81c784"}}>({t("TripAccepted")})</h5> :
                            <h5 style={{ color: "#f50057"}}>({t("TripNotYetAccepted")})</h5>
                        }
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
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                    <div className="pl-4 pb-4">
                        <img src={this.state.showDetails ? arrowUp : arrowDown} alt="lalal" style={{ height: '32px', width: '32px' }} onClick={this.handleShowDetailsChange} />
                    </div>
                </div>
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }} hidden={!this.state.showDetails}>
                    {this.showFlight(this.props.checklist[this.props.index])}
                    {this.showCar(this.props.checklist[this.props.index])}
                    <div className="col-12 col-lg-4" hidden={!this.state.apartmentsInfo.IsRequired}>
                        <h6> {t("ApartmentsAddres")}: {this.state.apartmentsInfo.AppartmentsAddress}</h6>

                    </div>
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
        checklist: state.checklist
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CheckListCard));

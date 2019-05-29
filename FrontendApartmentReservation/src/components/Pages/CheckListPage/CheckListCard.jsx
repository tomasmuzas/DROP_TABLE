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
import pdf from './pdf.png';
import { GridLoader } from 'react-spinners';
import {BACKEND_URL} from '../../../actions/index'

class CheckListCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showDetails: false,
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

    getFormattedDate(checklistDate){
        if(checklistDate){
            var tempDate = new Date(checklistDate);
             return tempDate.toLocaleDateString('lt-LT') + " " + tempDate.toLocaleTimeString('lt-LT', { hour: '2-digit', minute: '2-digit' });
        }
        else{
            return checklistDate;
        }
    }

    showFlight(checklist) {
        const { t } = this.props;
        if (checklist) {
            
            return (
                <div className="col-12 col-lg-4 pb-2" hidden={!checklist.flight.isRequired}>
                    <h5><b>{t("FlightInformation")}</b></h5>                                            
                    <h6> {t("FlightNumber")}: {checklist.flight.flightNumber}</h6>
                    <h6> {t("FlightCompany")}:  {checklist.flight.company}</h6>
                    <h6> {t("AirportAddress")}:  {checklist.flight.airportAddress}</h6>
                    <h6> {t("FlightTime")}: {this.getFormattedDate(checklist.flight.flightTime)} </h6>
                    <h6>                                    
                        {checklist.flight.ticketFileId &&
                            <a href={BACKEND_URL + '/files/' + checklist.flight.ticketFileId}>
                                {t("CurrentFlightTicket")} <img src={pdf} alt="pdf-icon" style={{ height: '16px' }}/>
                            </a>
                        }
                    </h6>
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

    showApartments(checklist) {
        const { t } = this.props;
        if (checklist) {
            if(checklist.livingPlace.apartmentReservationInfo.required){
                return (
                    <div className="col-12 col-lg-4 pb-2" hidden={!checklist.livingPlace.isRequired}>
                        <h5><b>{t("ApartmentInformation")}</b></h5>                        
                        <h6> {t("ApartmentAddress")}: {checklist.livingPlace.apartmentReservationInfo.apartmentAddress}</h6>
                        <h6> {t("RoomNumber")}:  {checklist.livingPlace.apartmentReservationInfo.roomNumber}</h6>
                        <h6> {t("DateFrom")}:  {this.getFormattedDate(checklist.livingPlace.apartmentReservationInfo.dateFrom)}</h6>
                        <h6> {t("DateTo")}: {this.getFormattedDate(checklist.livingPlace.apartmentReservationInfo.dateTo)} </h6>
                    </div>
                )
            }
            if(checklist.livingPlace.hotelReservationInfo.required){
                return (
                    <div className="col-12 col-lg-4 pb-2" hidden={!checklist.livingPlace.isRequired}>
                        <h5><b>{t("HotelInformation")}</b></h5>
                        <h6> {t("Hotel")}: {checklist.livingPlace.hotelReservationInfo.hotelName}</h6>
                        <h6> {t("DateFrom")}:  {this.getFormattedDate(checklist.livingPlace.hotelReservationInfo.dateFrom)}</h6>
                        <h6> {t("DateTo")}:  {this.getFormattedDate(checklist.livingPlace.hotelReservationInfo.dateTo)}</h6>
                        <h6>                                    
                            {checklist.livingPlace.hotelReservationInfo.documentsFileId &&
                                <a href={BACKEND_URL + '/files/' + checklist.livingPlace.hotelReservationInfo.documentsFileId}>
                                    {t("CurrentHotelDocuments")} <img src={pdf} alt="pdf-icon" style={{ height: '16px' }}/>
                                </a>
                            }
                        </h6>
                    </div>
                )
            }
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

    showCar(checklist) {
        const { t } = this.props;
        if (checklist) {
            return (
                <div className="col-12 col-lg-4 pb-2" hidden={!checklist.car.isRequired}>
                    <h5><b>{t("CarInformation")}</b></h5>
                    <h6> {t("CarNumber")}: {checklist.car.carNumber}</h6>
                    <h6> {t("CarAddress")}:  {checklist.car.carAddress}</h6>
                    <h6> {t("RentStartTime")}: {this.getFormattedDate(checklist.car.rentStartTime)}</h6>
                    <h6> {t("RentEndTime")}: {this.getFormattedDate(checklist.car.rentEndTime)}</h6>
                    <h6>                                    
                        {checklist.car.documentsFileId &&
                            <a href={BACKEND_URL + '/files/' + checklist.car.documentsFileId}>
                                {t("CurrentCarDocuments")} <img src={pdf} alt="pdf-icon" style={{ height: '16px' }}/>
                            </a>
                        }
                    </h6>
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
        const { checkListInfo, t, tripId } = this.props;
        return (
            <div>
                <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: "5pt 5pt 0 0"}}>
                    <div className="col justify-content-md-center nameDiv pb-lg-0 pb-5">
                        <h5><b>{checkListInfo.employee.firstName} {checkListInfo.employee.lastName} &nbsp;</b>
                        {checkListInfo.hasAcceptedTripConfirmation ?
                                <span style={{ color: "#81c784" }}>({t("TripAccepted")})</span> :
                                <span style={{ color: "#f50057" }}>({t("TripNotYetAccepted")})</span>
                        }
                        </h5>
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
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: this.state.showDetails? "0": "0 0 5pt 5pt" }}>
                    <div className="pl-3 pb-2">
                        <img src={this.state.showDetails ? arrowUp : arrowDown} alt="lalal" style={{ height: '32px', width: '32px' }} onClick={this.handleShowDetailsChange} />
                    </div>
                </div>
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: "0 0 5pt 5pt" }} hidden={!this.state.showDetails}>
                    {this.showFlight(this.props.checklist[this.props.index])}
                    {this.showCar(this.props.checklist[this.props.index])}
                    {this.showApartments(this.props.checklist[this.props.index])}
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

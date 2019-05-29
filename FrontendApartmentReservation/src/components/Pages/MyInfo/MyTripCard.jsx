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
import arrowUp from '../CheckListPage/up-arrow.png';
import arrowDown from '../CheckListPage/down-arrow.png';
import pdf from '../CheckListPage/pdf.png';
import { GridLoader } from 'react-spinners';
import Button from '@material-ui/core/Button';
import {BACKEND_URL} from '../../../actions/index'


class MyTripCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showDetails: false,
        }

        this.handleShowDetailsChange = this.handleShowDetailsChange.bind(this);
        this.acceptTrip = this.acceptTrip.bind(this);
    }

    acceptTrip(e) {
        var that = this;
        var promise = this.props.acceptTrip(this.props.trip.tripId).then(function (result) {
            if (result === 200) {
                that.setState({
                    showDetails: false,
                })
            }
        });
    }

    handleShowDetailsChange(e) {
        this.setState({
            showDetails: !this.state.showDetails
        })
    }

    componentWillMount() {
        this.props.getPersonalChecklist(this.props.myId, this.props.trip.tripId);
    }

    getFormattedDate(checklistDate){
        if(checklistDate){
            var tempDate = new Date(checklistDate);
             return tempDate.toLocaleDateString('lt-LT') + " " + tempDate.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' });
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
                        <h6> {t("DateTo")}:  {this.getFormattedDate(checklist.livingPlace.hotelReservationInfo.dateFrom)}</h6>
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
                    <h6> {t("RentEndTime")}: {this.getFormattedDate(checklist.car.rentEndtTime)}</h6>
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
        const { t, trip } = this.props;
        var tripStartTime = new Date(trip.startTime);
        var tripEndTime = new Date(trip.endTime);
        if (this.props.personalChecklist && this.props.personalChecklist !== []) {
            return (
                <div>
                    <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: "5pt 5pt 0 0" }}>
                        <div className="col justify-content-md-center nameDiv pb-lg-0 pb-5 pt-2">
                            <h4 ><b>{t("TripDestination")} {trip.office.address}</b></h4>
                            <h6 >{t("DepartureDate")}:  {tripStartTime.toLocaleDateString('lt-LT')}</h6>
                            <h6 >{t("ReturnDate")}:  {tripEndTime.toLocaleDateString('lt-LT')}</h6>
                            {trip.checklistInfos[0].hasAcceptedTripConfirmation ?
                                <h5 style={{ color: "#81c784" }}>({t("TripAccepted")})</h5> :
                                <h5 style={{ color: "#f50057" }}>({t("TripNotYetAccepted")})<span> </span>
                                    <Button className="mx-auto pl-3" variant="contained" color="secondary" onClick={this.acceptTrip}>
                                        {t("Accept")}
                                    </Button></h5>
                            }
                        </div>
                        <div className="col pr-5">
                            <div style={{ float: "right" }}>
                                <FormControlLabel
                                    control={<Checkbox checked={trip.checklistInfos[0].isApartmentRequired} />}
                                    label={t("ApartmentRequired")}
                                />
                                <FormControlLabel
                                    control={<Checkbox checked={trip.checklistInfos[0].isFlightRequired} />}
                                    label={t("FlightRequired")}
                                />
                                <FormControlLabel
                                    control={<Checkbox checked={trip.checklistInfos[0].isCarRentRequired} />}
                                    label={t("CarRequired")}
                                />
                            </div>
                        </div>

                    </div>
                    <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: this.state.showDetails? "0": "0 0 5pt 5pt" }}>
                        <div className="pl-3 pb-2">
                            <img src={this.state.showDetails ? arrowUp : arrowDown} alt="lalal" style={{ height: '32px', width: '32px' }} onClick={this.handleShowDetailsChange} />
                        </div>
                    </div>
                    <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E',  borderRadius: "0 0 5pt 5pt" }} hidden={!this.state.showDetails}>
                        {this.showFlight(this.props.personalChecklist[this.props.index])}
                        {this.showCar(this.props.personalChecklist[this.props.index])}
                        {this.showApartments(this.props.personalChecklist[this.props.index])}
                    </div>
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
}
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        personalChecklist: state.personalChecklist,
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(MyTripCard));

import React from 'react';
import i18next from 'i18next';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux'; import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';
import { GridLoader } from 'react-spinners';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import moment from 'moment'
import { BACKEND_URL } from '../../../../actions/index'
import pdf from '../pdf.png';

class ApartmentsCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            livingPlace: {
                isRequired: false,
                apartmentReservationInfo: { required: false, apartmentAddress: undefined, roomNumber: 1, dateFrom: null, dateTo: null },
                hotelReservationInfo: { required: false, hotelName: '', dateFrom: '', dateTo: '', timeFrom: '', timeTo: '', file: null, price: '' }
            },
            showHotelInfo: false,
            showApartmentsInfo: true,
            showBookHotelButton: true,
        }

        this.handleHotelSubmit = this.handleHotelSubmit.bind(this);
        this.handleHotelDocumentsSubmit = this.handleHotelDocumentsSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleHotelNameChange = this.handleHotelNameChange.bind(this);
        this.handleHotelPriceChange = this.handleHotelPriceChange.bind(this);
        this.handleHotelDocumentsChange = this.handleHotelDocumentsChange.bind(this);
        this.deleteReservation = this.deleteReservation.bind(this);
        this.reserveApartmentForOne = this.reserveApartmentForOne.bind(this);
        this.handleDateFromChange = this.handleDateFromChange.bind(this);
        this.handleDateToChange = this.handleDateToChange.bind(this);
        this.handleTimeFromChange = this.handleTimeFromChange.bind(this);
        this.handleTimeToChange = this.handleTimeToChange.bind(this);
        this.bookHotel = this.bookHotel.bind(this);
        this.deleteHotelReservation = this.deleteHotelReservation.bind(this);
    }

    componentWillReceiveProps(newProps) {
        var showBookHotelButton = !newProps.apartmentsInfo.apartmentReservationInfo.apartmentAddress && !newProps.apartmentsInfo.hotelReservationInfo.hotelName ? true : false;
        var showApartmentsInfo = (newProps.apartmentsInfo.apartmentReservationInfo.apartmentAddress && !newProps.apartmentsInfo.hotelReservationInfo.hotelName) ||
            (!newProps.apartmentsInfo.apartmentReservationInfo.apartmentAddress && !newProps.apartmentsInfo.hotelReservationInfo.hotelName) ? true : false;
        var showHotelInfo = !!newProps.apartmentsInfo.hotelReservationInfo.hotelName;

        var currentState = newProps.apartmentsInfo;
        if (newProps.apartmentsInfo.hotelReservationInfo.dateFrom) {
            var rentFromArray = newProps.apartmentsInfo.hotelReservationInfo.dateFrom.split('T');
            currentState.hotelReservationInfo.dateFrom = rentFromArray[0];
            currentState.hotelReservationInfo.timeFrom = rentFromArray[1];
        }

        if (newProps.apartmentsInfo.hotelReservationInfo.dateTo) {
            var rentToArray = newProps.apartmentsInfo.hotelReservationInfo.dateTo.split('T');
            currentState.hotelReservationInfo.dateTo = rentToArray[0];
            currentState.hotelReservationInfo.timeTo = rentToArray[1];
        }

        this.setState({
            livingPlace: currentState,
            apartmentsSpace: this.props.tripbasic.availableApartments,
            showBookHotelButton: showBookHotelButton,
            showApartmentsInfo: showApartmentsInfo,
            showHotelInfo: showHotelInfo
        })
    }

    handleHotelDocumentsSubmit(e) {
        e.preventDefault();
        const { file } = this.state.livingPlace.hotelReservationInfo;

        if (file) {
            this.props.uploadHotelDocuments(file, this.props.employeeId, this.props.tripId)
        }
    }

    handleHotelSubmit(e) {
        e.preventDefault();
        var { hotelName, dateFrom, dateTo, timeTo, timeFrom, price } = this.state.livingPlace.hotelReservationInfo;
        if (!!dateFrom && !!timeFrom) {
            dateFrom = dateFrom + ' ' + timeFrom;
        }

        if (!!dateTo && !!timeTo) {
            dateTo = dateTo + ' ' + timeTo;
        }

        this.props.updateApartmentsInfo(hotelName, dateFrom, dateTo, this.props.tripId, this.props.employeeId, price);
    }

    deleteReservation() {
        var that = this;
        var promise = this.props.deleteApartmentsReservationForOne(this.props.employeeId, this.props.tripId).then(function (result) {
            if (result === 200) {
                var tempState = that.state.livingPlace;
                tempState.apartmentReservationInfo.apartmentAddress = null;
                that.setState({
                    livingPlace: tempState,
                    showBookHotelButton: true
                })
            }
        });
    }

    deleteHotelReservation() {
        var that = this;
        var promise = this.props.deleteHotelReservation(this.props.employeeId, this.props.tripId).then(function (result) {
            if (result === 200) {
                var tempState = that.state.livingPlace;
                tempState.apartmentReservationInfo.apartmentAddress = null;
                that.setState({
                    livingPlace: tempState,
                    showBookHotelButton: true,
                    showHotelInfo: false,
                    showApartmentsInfo: true
                })
            }
        });
    }

    reserveApartmentForOne() {
        var that = this;
        var promise = this.props.reserveApartmentsForOne(this.props.employeeId, this.props.tripId).then(function (result) {
            if (result === 200) {
                var tempState = that.state.livingPlace;
                tempState.apartmentReservationInfo.apartmentAddress = 'set';
                that.setState({
                    livingPlace: tempState,
                    showBookHotelButton: false,
                })
            }
        });
    }

    handleIsRequiredChange(e) {
        if (!e.target.checked) {
            this.props.deleteApartmentsInfo(this.props.employeeId, this.props.tripId);
            this.setState({
                showApartmentsInfo: true,
                showHotelInfo: false,
                showBookHotelButton: false,
            })

        }
        var livingPlace = this.state.livingPlace;
        livingPlace.isRequired = e.target.checked;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleHotelNameChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.hotelName = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleDateFromChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.dateFrom = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleTimeFromChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.timeFrom = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleDateToChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.dateTo = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleTimeToChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.timeTo = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }

    handleHotelPriceChange(e) {
        var livingPlace = this.state.livingPlace;
        livingPlace.hotelReservationInfo.price = e.target.value;
        this.setState({
            livingPlace: livingPlace
        });
    }


    handleHotelDocumentsChange(e) {
        var livingPlace = this.state.livingPlace;
        var hotelInfo = livingPlace.hotelReservationInfo;
        hotelInfo.file = e.target.files[0];
        this.setState({
            livingPlace: livingPlace
        });
    }

    bookHotel() {
        this.props.createApartmentsInfo(this.props.employeeId, this.props.tripId);
        this.setState({
            showApartmentsInfo: false,
            showHotelInfo: true,
            showBookHotelButton: false,
        })
    }

    hotelForm() {
        return (
            <div>
                <button className={`btn btn-lg btn-primary btn-block`} hidden={!this.state.showBookHotelButton} onClick={this.bookHotel}>{i18next.t("BookHotel")}</button>
                <form className={`form-signin`} onSubmit={this.handleHotelSubmit} hidden={!this.state.showHotelInfo} >
                    <div className="form-group mb-2">
                        {i18next.t("Hotel")}
                        <input type="text" id="Hotel" className={`form-control`} placeholder={i18next.t("Hotel")}
                            autoFocus name="Hotel" value={this.state.livingPlace.hotelReservationInfo.hotelName}
                            onChange={this.handleHotelNameChange} />
                    </div>
                    <div className="form-group mb-2">
                        {i18next.t("DateFrom")}
                        <input type="date" id="DateFrom" className={`form-control`} placeholder={i18next.t("DateFrom")}
                            name="DateFrom" value={this.state.livingPlace.hotelReservationInfo.dateFrom}
                            onChange={this.handleDateFromChange} />
                    </div>
                    <div className="form-group mb-2">
                        {i18next.t("DateFrom")}
                        <input type="time" id="TimeFrom" className={`form-control`} placeholder={i18next.t("DateFrom")}
                            name="DateFrom" value={this.state.livingPlace.hotelReservationInfo.timeFrom}
                            onChange={this.handleTimeFromChange} />
                    </div>
                    <div className="form-group mb-2">
                        {i18next.t("DateTo")}
                        <input type="date" id="DateTo" className={`form-control`} placeholder={i18next.t("DateTo")}
                            name="DateTo" value={this.state.livingPlace.hotelReservationInfo.dateTo}
                            onChange={this.handleDateToChange} />
                    </div>
                    <div className="form-group mb-2">
                        {i18next.t("DateTo")}
                        <input type="time" id="TimeTo" className={`form-control`} placeholder={i18next.t("DateTo")}
                            name="DateTo" value={this.state.livingPlace.hotelReservationInfo.timeTo}
                            onChange={this.handleTimeToChange} />
                    </div>
                    <div className="form-group">
                                {i18next.t("Price")}
                                <input type="number" id="HotelPrice" className={`form-control`} placeholder={i18next.t("Price")}
                                    name="HotelPrice" value={this.state.livingPlace.hotelReservationInfo.price}
                                    onChange={this.handleHotelPriceChange} />
                            </div>
                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">{i18next.t("SaveHotelInfo")}</button>
                    <button className={`btn btn-lg btn-primary btn-block`} onClick={this.deleteHotelReservation}>{i18next.t("DeleteHotelInfo")}</button>
                </form>

                <form className={`form-signin`} encType="multipart/form-data" hidden={!this.state.showHotelInfo} onSubmit={this.handleHotelDocumentsSubmit}>
                    <div className="form-group">
                        <h3>{i18next.t("HotelDocuments")}</h3>
                        <div className="mt-4 mb-4">
                            {this.state.livingPlace.hotelReservationInfo.documentsFileId &&
                                <a href={BACKEND_URL + '/files/' + this.state.livingPlace.hotelReservationInfo.documentsFileId}>
                                    {i18next.t("CurrentHotelDocuments")} <img src={pdf} alt="pdf-icon" style={{ height: '32px' }} />
                                </a>
                            }
                        </div>
                        <input type="file" accept="application/pdf" id="FlightTicket" className={`form-control`}
                            name="HotelDocument"
                            onChange={this.handleHotelDocumentsChange} />
                    </div>

                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">{i18next.t("SaveHotelDocuments")}</button>
                </form>
            </div>
        )
    }

    apartmentsForm() {
        if (this.state.livingPlace.apartmentReservationInfo.apartmentAddress) {
            return (
                <span className="row" style={{ "display": "inline-block" }}>
                    {i18next.t("ReservationAlreadyMade")}
                    <button className={`btn btn-lg btn-primary btn-block`} onClick={this.deleteReservation}>{i18next.t("DeleteReservation")}</button>
                </span>
            )
        }
        else {
            return (
                <button className={`btn btn-lg btn-primary btn-block`} onClick={this.reserveApartmentForOne}>{i18next.t("ReserveApartmentForOne")}</button>
            )
        }
    }

    livingPlaceForm() {
        return (
            <div className="row">
                <div className={this.state.livingPlace.apartmentReservationInfo.apartmentAddress ? "col-12" : "col-6" + " form-group mb-2 mt-2"} hidden={!this.state.showApartmentsInfo}>
                    {this.apartmentsForm()}
                </div>
                <div className={this.state.showHotelInfo ? "col-12" : "col-6" + " form-group mb-2 mt-2 "}>
                    {this.hotelForm()}
                </div>
            </div>
        )

    }

    render() {

        const { t } = this.props;
        if (this.state.livingPlace) {
            return (
                <div className=" jumbotron mx-auto col-12 col-lg-6 pb-0 mt-0 pt-0 px-0">
                    <div style={{ borderBottom: '1px solid #b4bac4' }}>
                        <label className='pt-2 pl-2'>{t("ApartmentRequired")}</label>
                        <Checkbox style={{ float: 'right' }} checked={this.state.livingPlace.isRequired} onChange={this.handleIsRequiredChange} />
                    </div>
                    <div className={`loginForm text-center jumbotron mx-auto col-12 pb-1 mt-0 pt-0`} hidden={!this.state.livingPlace.isRequired}>
                        {this.livingPlaceForm()}
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
        user: state.user,
        tripbasic: state.tripbasic
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(ApartmentsCheckList));
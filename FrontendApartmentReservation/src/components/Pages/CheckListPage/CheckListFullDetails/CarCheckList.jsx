import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux'; import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';
import { GridLoader } from 'react-spinners';
import moment from 'moment'
import { BACKEND_URL } from '../../../../actions/index'
import pdf from '../pdf.png';


class CarCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            carInfo: {
                isRequired: false,
                carNumber: '',
                carAddress: '',
                rentStartTime: '',
                rentEndTime: '',
                rentStartDate: '',
                rentEndDate: '',
                cost: '',
            },
            carDocuments: {
                file: null
            }
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleDocumentSubmit = this.handleDocumentSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleCarNumberChange = this.handleCarNumberChange.bind(this);
        this.handleCarAddressChange = this.handleCarAddressChange.bind(this);
        this.handleCarPriceChange = this.handleCarPriceChange.bind(this);
        this.handleRentStartDateChange = this.handleRentStartDateChange.bind(this);
        this.handleRentStartTimeChange = this.handleRentStartTimeChange.bind(this);
        this.handleRentEndDateChange = this.handleRentEndDateChange.bind(this);
        this.handleRentEndTimeChange = this.handleRentEndTimeChange.bind(this);
        this.handleDocumentChange = this.handleDocumentChange.bind(this);
    }

    componentWillReceiveProps(newProps) {
        var currentState = newProps.carInfo;
        if (newProps.carInfo.rentStartTime) {
            var rentStartDateArray = newProps.carInfo.rentStartTime.split('T');
            currentState.rentStartDate = rentStartDateArray[0];
            currentState.rentStartTime = rentStartDateArray[1];
        }

        if (newProps.carInfo.rentEndTime) {
            var rentEndDateArray = newProps.carInfo.rentEndTime.split('T');
            currentState.rentEndDate = rentEndDateArray[0];
            currentState.rentEndTime = rentEndDateArray[1];
        }

        this.setState({
            carInfo: currentState
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { carInfo } = this.state;
        this.props.updateCarInfo(carInfo, this.props.employeeId, this.props.tripId);
    }

    handleDocumentSubmit(e) {
        e.preventDefault();
        const { carDocuments } = this.state;
        if (this.state.carDocuments.file) {
            this.props.updateCarDocuments(carDocuments.file, this.props.employeeId, this.props.tripId);
        }
    }

    handleIsRequiredChange(e) {
        if (e.target.checked) {
            this.props.createCarInfo(this.props.employeeId, this.props.tripId);
        }
        else {
            this.props.deleteCarInfo(this.props.employeeId, this.props.tripId);
        }
        var carInfo = this.state.carInfo;
        carInfo.isRequired = e.target.checked;
        this.setState({
            carInfo: carInfo
        });
    }

    handleCarNumberChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.carNumber = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleCarAddressChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.carAddress = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleRentStartDateChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.rentStartDate = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleRentStartTimeChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.rentStartTime = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleRentEndDateChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.rentEndDate = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleRentEndTimeChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.rentEndTime = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    handleDocumentChange(e) {
        var documents = this.state.carDocuments;
        documents.file = e.target.files[0];
        this.setState({
            carDocuments: documents
        });
    }

    handleCarPriceChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.cost = e.target.value;
        this.setState({
            carInfo: carInfo
        }); 
    }

    render() {
        const { t } = this.props;
        if (this.state.carInfo) {
            return (
                <div className=" jumbotron mx-auto col-12 col-lg-6 pb-0 mt-0 pt-0 px-0">
                    <div style={{ borderBottom: '1px solid #b4bac4' }}>
                        <label className='pt-2 pl-2'>{t("CarRequired")}</label>
                        <Checkbox style={{ float: 'right' }} checked={this.state.carInfo.isRequired} onChange={this.handleIsRequiredChange} />
                    </div>

                    <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 mt-0 pt-0`} hidden={!this.state.carInfo.isRequired}>
                        <form className={`form-signin`} onSubmit={this.handleSubmit} >
                            <div className="form-group mb-2">
                                {t("CarNumber")}
                                <input type="text" id="CarNumber" className={`form-control`} placeholder={t("CarNumber")}
                                    autoFocus name="CarNumber" value={this.state.carInfo.carNumber}
                                    onChange={this.handleCarNumberChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("CarAddress")}
                                <input type="text" id="CarAddress" className={`form-control`} placeholder={t("CarAddress")}
                                    name="CarAddress" value={this.state.carInfo.carAddress}
                                    onChange={this.handleCarAddressChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentStartTime")}
                                <input type="date" id="RentStartDate" className={`form-control`} placeholder={t("RentStartTime")}
                                    name="RentStartTime" value={this.state.carInfo.rentStartDate}
                                    onChange={this.handleRentStartDateChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentStartTime")}
                                <input type="time" id="RentStartTimeHours" className={`form-control`} placeholder={t("RentStartTime")}
                                    name="RentStartTime" value={this.state.carInfo.rentStartTime}
                                    onChange={this.handleRentStartTimeChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentEndTime")}
                                <input type="date" id="RentEndTime" className={`form-control`} placeholder={t("RentEndTime")}
                                    name="RentEndTime" value={this.state.carInfo.rentEndDate}
                                    onChange={this.handleRentEndDateChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentEndTime")}
                                <input type="time" id="RentEndTimeHours" className={`form-control`} placeholder={t("RentEndTime")}
                                    name="RentEndTime" value={this.state.carInfo.rentEndTime}
                                    onChange={this.handleRentEndTimeChange} />
                            </div>
                            <div className="form-group">
                                {t("Price")}
                                <input type="number" id="CarPrice" className={`form-control`} placeholder={t("Price")}
                                    name="CarPrice" value={this.state.carInfo.cost}
                                    onChange={this.handleCarPriceChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveCarInfo")}</button>
                        </form>

                        <form className={`form-signin`} encType="multipart/form-data" onSubmit={this.handleDocumentSubmit}>
                            <div className="form-group">
                                <h3>{t("CarDocuments")}</h3>
                                {this.state.carInfo.documentsFileId &&
                                    <div className="mt-4 mb-4">
                                        <a href={BACKEND_URL + '/files/' + this.state.carInfo.documentsFileId}>
                                            {t("CurrentCarDocuments")}  <img src={pdf} alt="pdf-icon" style={{ height: '32px' }} />
                                        </a>
                                    </div>
                                }
                                <input type="file" accept="application/pdf" id="FlightTicket" className={`form-control`}
                                    name="Document"
                                    onChange={this.handleDocumentChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveDocuments")}</button>
                        </form>
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
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CarCheckList));
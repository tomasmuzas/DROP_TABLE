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
import {BACKEND_URL} from '../../../../actions/index'


class CarCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            carInfo: {
                isRequired: true,
                carNumber: '',
                carAddress: '',
                rentStartTime: '',
                rentEndTime: '',
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
        this.handleRentStartTimeChange = this.handleRentStartTimeChange.bind(this);
        this.handleRentEndTimeChange = this.handleRentEndTimeChange.bind(this);
        this.handleDocumentChange = this.handleDocumentChange.bind(this);
    }

    componentWillReceiveProps(newProps) {
        this.setState({
            carInfo: newProps.carInfo
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
        if(this.state.carDocuments.file){
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

    handleRentStartTimeChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.rentStartTime = e.target.value;
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
                                    required autoFocus name="CarNumber" value={this.state.carInfo.carNumber}
                                    onChange={this.handleCarNumberChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("CarAddress")}
                                <input type="text" id="CarAddress" className={`form-control`} placeholder={t("CarAddress")}
                                    required name="CarAddress" value={this.state.carInfo.carAddress}
                                    onChange={this.handleCarAddressChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentStartTime")}
                                <input type="datetime-local" id="RentStartTime" className={`form-control`} placeholder={t("RentStartTime")}
                                    required name="RentStartTime" value={moment(this.state.carInfo.rentEndTime).format('YYYY-MM-DDTHH:MM')}
                                    onChange={this.handleRentStartTimeChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentEndTime")}
                                <input type="datetime-local" id="RentEndTime" className={`form-control`} placeholder={t("RentEndTime")}
                                    required name="RentEndTime" value={moment(this.state.carInfo.rentEndTime).format('YYYY-MM-DDTHH:MM')}
                                    onChange={this.handleRentEndTimeChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveCarInfo")}</button>
                        </form>

                        <form className={`form-signin`} encType= "multipart/form-data" onSubmit={this.handleDocumentSubmit}>
                            <div className="form-group">
                                {t("CarDocument")}
                                <input type="file" accept="application/pdf" id="FlightTicket" className={`form-control`}
                                    name="Document"
                                    onChange={this.handleDocumentChange} />
                            </div>
                            {this.state.carInfo.documentFileId &&
                                <a href={BACKEND_URL + '/files/' + this.state.carInfo.documentFileId}>
                                    Dabartiniai dokumentai
                                </a>
                            }
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveTicket")}</button>                            
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
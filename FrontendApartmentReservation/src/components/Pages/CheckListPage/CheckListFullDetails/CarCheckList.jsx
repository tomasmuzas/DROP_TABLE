import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';

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
            }
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleCarNumberChange = this.handleCarNumberChange.bind(this);
        this.handleCarAddressChange = this.handleCarAddressChange.bind(this);
        this.handleRentStartTimeChange = this.handleRentStartTimeChange.bind(this);
        this.handleRentEndTimeChange = this.handleRentEndTimeChange.bind(this);

    }

    componentWillReceiveProps(newProps){
        this.setState({
            carInfo: newProps.carInfo
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { carInfo } = this.state;
        this.props.updateCarInfo(carInfo, this.props.employeeId, this.props.tripId);
    }

    handleIsRequiredChange(e) {
        if(e.target.checked){
            this.props.createCarInfo(this.props.employeeId, this.props.tripId);
        }
        else{
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
                                    required name="RentStartTime" value={this.state.carInfo.rentStartTime}
                                    onChange={this.handleRentStartTimeChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("RentEndTime")}
                                <input type="datetime-local" id="RentEndTime" className={`form-control`} placeholder={t("RentEndTime")}
                                    required name="RentEndTime" value={this.state.carInfo.rentEndTime}
                                    onChange={this.handleRentEndTimeChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveCarInfo")}</button>
                        </form>
                    </div>
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
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CarCheckList));
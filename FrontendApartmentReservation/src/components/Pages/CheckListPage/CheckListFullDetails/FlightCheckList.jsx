import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';

class FlightCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            flightInfo: {
                isRequired: true,
                flightNumber: '',
                company: '',
                flightTime: '',
                airportAddress: '',
            }
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleFlightNumberChange = this.handleFlightNumberChange.bind(this);
        this.handleAirportAddressChange = this.handleAirportAddressChange.bind(this);
        this.handleFlightCompanyChange = this.handleFlightCompanyChange.bind(this);
        this.handleFlightTimeChange = this.handleFlightTimeChange.bind(this);
    }

    componentWillReceiveProps(newProps){
        this.setState({
            flightInfo: newProps.singleFlightInfo
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { flightInfo } = this.state;
        this.props.updateFlightInfo(flightInfo, this.props.employeeId, this.props.tripId);
    }

    handleIsRequiredChange(e) {
        if(e.target.checked){
            this.props.createFlightInfo(this.props.employeeId, this.props.tripId);
        }
        else{
            this.props.deleteFlightInfo(this.props.employeeId, this.props.tripId);
        }
        var flightInfo = this.state.flightInfo;
        flightInfo.isRequired = e.target.checked;
        this.setState({
            flightInfo: flightInfo
        });
    }

    handleFlightNumberChange(e) {
        var flightInfo = this.state.flightInfo;
        flightInfo.flightNumber = e.target.value;
        this.setState({
            flightInfo: flightInfo
        });
    }

    handleAirportAddressChange(e) {
        var flightInfo = this.state.flightInfo;
        flightInfo.airportAddress = e.target.value;
        this.setState({
            flightInfo: flightInfo
        });
    }

    handleFlightCompanyChange(e) {
        var flightInfo = this.state.flightInfo;
        flightInfo.company = e.target.value;
        this.setState({
            flightInfo: flightInfo
        });
    }

    handleFlightTimeChange(e) {
        var flightInfo = this.state.flightInfo;
        flightInfo.flightTime = e.target.value;
        this.setState({
            flightInfo: flightInfo
        });
    }

    render() {
        const { t } = this.props;
        if (this.state.flightInfo) {
            return (
                <div className=" jumbotron mx-auto col-12 col-lg-6 pb-0 mt-0 pt-0 px-0">
                    <div style={{ borderBottom: '1px solid #b4bac4' }}>
                        <label className='pt-2 pl-2'>{t("FlightRequired")}</label>
                        <Checkbox style={{ float: 'right' }} checked={this.state.flightInfo.isRequired} onChange={this.handleIsRequiredChange} />
                    </div>

                    <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 mt-0 pt-0`} hidden={!this.state.flightInfo.isRequired}>
                        <form className={`form-signin`} onSubmit={this.handleSubmit} >
                            <div className="form-group mb-2">
                                {t("FlightNumber")}
                                <input type="text" id="FlightNumber" className={`form-control`} placeholder={t("FlightNumber")}
                                    required autoFocus name="FlightNumber" value={this.state.flightInfo.flightNumber}
                                    onChange={this.handleFlightNumberChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("FlightTime")}
                                <input type="datetime-local" id="FlightTime" className={`form-control`} placeholder={t("FlightTime")}
                                    required name="FlightTime" value={this.state.flightInfo.flightTime}
                                    onChange={this.handleFlightTimeChange} />
                            </div>
                            <div className="form-group mb-2">
                                {t("AirportAddress")}
                                <input type="text" id="AirportAddress" className={`form-control`} placeholder={t("AirportAddress")}
                                    required name="AirportAddress" value={this.state.flightInfo.airportAddress}
                                    onChange={this.handleAirportAddressChange} />
                            </div>
                            <div className="form-group">
                                {t("FlightCompany")}
                                <input type="text" id="FlightCompany" className={`form-control`} placeholder={t("FlightCompany")}
                                    required name="FlightCompany" value={this.state.flightInfo.company}
                                    onChange={this.handleFlightCompanyChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveFlightInfo")}</button>
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

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(FlightCheckList));
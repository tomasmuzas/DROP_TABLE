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

class CheckListCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showDetails: false,
            flightInfo: { IsRequired: true, FlightNumber: 12342, FlightTime: '2019-05-01T11:11', AirportAddress: 'Didlaukio g. 59', FlightCompany: 'SAS' },
        }

        this.handleShowDetailsChange = this.handleShowDetailsChange.bind(this);
    }

    handleShowDetailsChange(e) {
        this.setState({
            showDetails: e.target.checked
        })
    }

    componentWillMount() {
        //this.props.employeeId;
        //this.props.tripId;
        //getchecklistinfo
    }

    render() {
        const { checkListInfo, t, tripId } = this.props;
        return (
            <div>
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
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }}>
                    <Checkbox checked={this.state.showDetails} onChange={this.handleShowDetailsChange} />
                    <label className="pt-2 mt-1">Show details </label>
                </div>
                <div className="row mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E' }} hidden={!this.state.showDetails}>
                    <div className = "col-4" hidden={!this.state.flightInfo.IsRequired}>
                        <h6> Flight number: {this.state.flightInfo.FlightNumber}</h6>
                        <h6> Flight company: {this.state.flightInfo.FlightCompany}</h6>
                        <h6> Airport address: {this.state.flightInfo.AirportAddress}</h6>
                        <h6> Flight Date : {this.state.flightInfo.FlightTime}</h6>
                    </div>
                    <div className = "col-4" hidden={!this.state.flightInfo.IsRequired}>
                        <h6> Flight number: {this.state.flightInfo.FlightNumber}</h6>
                        <h6> Flight company: {this.state.flightInfo.FlightCompany}</h6>
                        <h6> Airport address: {this.state.flightInfo.AirportAddress}</h6>
                        <h6> Flight Date : {this.state.flightInfo.FlightTime}</h6>
                    </div>
                    <div className = "col-4" hidden={!this.state.flightInfo.IsRequired}>
                        <h6> Flight number: {this.state.flightInfo.FlightNumber}</h6>
                        <h6> Flight company: {this.state.flightInfo.FlightCompany}</h6>
                        <h6> Airport address: {this.state.flightInfo.AirportAddress}</h6>
                        <h6> Flight Date : {this.state.flightInfo.FlightTime}</h6>
                    </div>
                </div>
            </div>
        )
    }
}
export default withTranslation()(CheckListCard);
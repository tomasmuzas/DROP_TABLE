import React from 'react';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import Select from 'react-select'
import DatePicker from 'react-datepicker'
import * as actionCreators from '../../../actions';
import TextField from '@material-ui/core/TextField';


class CreateTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputCost: '',
            departureTime: '',
            returnDate: '',
            employeesOptions: [],
            officesOptions: []
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleCostChange = this.handleCostChange.bind(this);
        this.handleSurnameChange = this.handleSurnameChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);

    }

    componentWillMount() {
        this.props.getAllEmployees();
        //this.props.getAllOffices();
        var employeesOptions = [];
        var officeOptions = [];
        var employers = [
            { Id: 1, FirstName: "Martynas", LastName: "Narijauskas" },
            { Id: 2, FirstName: "Tomas", LastName: "Mūžas" },
            { Id: 3, FirstName: "Andrėjus", LastName: "Kuznecovas" }
        ];
        employers.forEach(function (employee) {
            var obj = { value: employee.Id, label: employee.FirstName + " " + employee.LastName }
            employeesOptions.push(obj);
        });

        var dbOffices = [
            { Id: 1, Address: "Vilnius didlaukio 59" },
            { Id: 2, Address: "Kaunas žalgirio 25" },
            { Id: 3, Address: "Londonas main square" }
        ]

        dbOffices.forEach(function (office) {
            var obj = { value: office.Id, label: office.Address };
            officeOptions.push(obj);
        });
        this.setState({
            employeesOptions: employeesOptions,
            officesOptions: officeOptions
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { inputName, inputSurname, inputEmail, inputPassword, inputOffice } = this.state;
            this.props.signUpUser(inputName, inputSurname, inputEmail, inputPassword, inputOffice);
            //create trip/
            //create group
    }

    handleCostChange(e) {
        this.setState({
            inputCost: e.target.value
        });
    }

    handleSurnameChange(e) {
        this.setState({
            inputSurname: e.target.value
        });
    }

    handleEmailChange(e) {
        this.setState({
            inputEmail: e.target.value
        });
    }

    handlePasswordChange(e) {
        this.setState({
            inputPassword: e.target.value
        });
    }

    handleOfficeChange(e) {
        this.setState({
            inputOffice: e.target.value
        });
    }

    render() {
        const { inputCost, inputSurname, inputEmail, inputPassword, inputOffice } = this.state;
        const { t } = this.props;
        console.log(this.state.officesOptions);
        console.log(this.state.employeesOptions);
        return (
            <div className={`loginForm text-center jumbotron mx-auto col-12 pb-1 pt-4 row`}>
                <form className={`form-signin col-6`} onSubmit={this.handleSubmit}>
                    <div className="form-group mb-2">
                        <input type="text" id="inputCost" className={`form-control`} placeholder={t("Cost")}
                            required autoFocus name="inputCost" value={inputCost}
                            onChange={this.handleCostChange} />
                    </div>
                    <div className="form-group mb-2">
                        <Select
                            isMulti
                            options={this.state.employeesOptions}
                            className="basic-multi-select"
                            placeholder="Select employees" />
                    </div>
                    <div className="form-group mb-2">
                        <Select
                            options={this.state.officesOptions}
                            className="basic-multi-select"
                            placeholder="Select office" />
                    </div>
                    <div className="row">
                        <div className="col-6">
                            <TextField
                                id="departureDate"
                                label={t("DepartureDate")}
                                type="date"
                                defaultValue="2017-05-24"
                                InputLabelProps={{
                                    shrink: true,
                                }} />
                        </div>
                        <div className="form-group col-6">
                            <TextField
                                id="ReturnDate"
                                label={t("ReturnDate")}
                                type="date"
                                defaultValue="2017-05-24"
                                InputLabelProps={{
                                    shrink: true,
                                }} />
                        </div>
                    </div>
                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("CreateTrip")}</button>
                </form>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        employees: state.employees
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CreateTrip));
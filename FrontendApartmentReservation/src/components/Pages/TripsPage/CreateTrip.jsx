import React from 'react';
import i18next from 'i18next';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import Select from 'react-select'
import * as actionCreators from '../../../actions';
import TextField from '@material-ui/core/TextField';
import SimpleReactCalendar from 'simple-react-calendar'
import { GridLoader } from 'react-spinners';

class CreateTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            departureDate: '',
            returnDate: '',
            employeesOptions: [],
            officesOptions: [],
            employeesPulled: false,
            selectedEmployees: [],
            selectedOffice: ''
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleEmployeesChange = this.handleEmployeesChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);
        this.handleDateSelect = this.handleDateSelect.bind(this);
    }

    componentWillMount() {
        this.props.getAllOffices();
        this.props.getEmployees();
    }

    componentWillReceiveProps(newProps) {
        this.setEmployeesArray(newProps.employees);
        this.setOfficesArray(newProps.offices);
    }

    setEmployeesArray(employeesList) {
        if (employeesList && employeesList.length !== 0) {
            var employeesOptions = [];
            employeesList.forEach(function (employee) {
                var obj = { value: employee.id, label: employee.firstName + " " + employee.lastName }
                employeesOptions.push(obj);
            });

            this.setState({
                employeesOptions: employeesOptions,
                employeesPulled: true
            })
        }
    }

    setOfficesArray(officesList) {
        var officesOptions = [];
        officesList.forEach(function (office) {
            var obj = { value: office.id, label: office.address };
            officesOptions.push(obj);
        });
        this.setState({
            officesOptions: officesOptions
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { departureDate, returnDate, selectedEmployees, selectedOffice } = this.state;
        this.props.createTrip(selectedEmployees, selectedOffice, departureDate, returnDate);

    }

    handleEmployeesChange(e) {
        var employeesIdArray = [];
        e.forEach(function (employee) {
            employeesIdArray.push(employee.value);
        })
        this.setState({
            selectedEmployees: employeesIdArray
        })
        this.props.getPlans(employeesIdArray);
    }

    handleOfficeChange(e) {
        this.setState({
            selectedOffice: e.value
        })
    }

    handleDateSelect(e) {
        this.setState({
            departureDate: e.start,
            returnDate: e.end
        })
    }

    getErrorMessage() {
        return (
            <div className="date_picker-notice">
                {i18next.t("ErrorDateMessage")}
            </div>
        )
    }

    getCalendar() {
        if (this.props.plans && this.state.selectedEmployees.length > 0) {
            return (
                <SimpleReactCalendar blockClassName="date_picker" activeMonth={new Date()}
                    NoticeComponent={this.getErrorMessage}
                    mode="range"
                    onSelect={this.handleDateSelect}
                    selected = {{"start": this.state.departureDate, "end": this.state.returnDate}}
                    disabledIntervals={this.props.plans}
                    daysOfWeek={[i18next.t("Monday"), i18next.t("Tuesday"), i18next.t("Wednesday"),
                    i18next.t("Thursday"), i18next.t("Friday"), i18next.t("Saturday"), i18next.t("Sunday")]}
                />
            )
        }
        else if (this.state.selectedEmployees.length <= 0) {
            return (
                <div>
                    <h5> {i18next.t("SelectEmployees")} </h5>
                </div>
            )
        }
        else if (!this.props.plans && this.state.selectedEmployees.length > 0) {
            return (
                <div className='center-div'>
                    <GridLoader
                        sizeUnit={"px"}
                        size={50}
                        color={'red'}
                    />
                </div>
            )
        }
    }

    render() {
        const { t } = this.props;

        return (
            <div className={`mx-auto pb-1 pt-4`} >
                <form className={`row`} onSubmit={this.handleSubmit}>
                    <div className="mb-2 p-5 col-12 col-lg-6">
                        <Select
                            isMulti
                            options={this.state.employeesOptions}
                            className="basic-multi- p-4"
                            placeholder={t("SelectEmployees")}
                            onChange={this.handleEmployeesChange}
                            required />
                        <Select
                            options={this.state.officesOptions}
                            className="basic-multi-select p-4"
                            placeholder={t("SelectOffice")}
                            onChange={this.handleOfficeChange}
                            required
                        />
                    </div>
                    <div className="col-12 col-lg-6" style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                        {this.getCalendar()}
                    </div>
                    <button className={`btn btn-lg btn-primary btn-block mx-auto m-5`} style={{ width: '30vh' }} type="submit">{t("CreateTrip")}</button>
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
        employees: state.employees,
        offices: state.offices,
        trips: state.trips,
        plans: state.plans
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CreateTrip));
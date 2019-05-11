import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../../actions';
import FlightCheckList from './FlightCheckList';
import CarCheckList from './CarCheckList';
import ApartmentsCheckList from './ApartmentsCheckList';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

class CheckListDetails extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            carInfo: { IsRequired: true, CarNumber: 1234 },
            apartmentsInfo: { IsRequired: true, Address: 1234 },

        }
    }


    componentWillMount() {
        this.props.getSingleFlightInfo(this.props.match.params.employeeId, this.props.match.params.tripId);
        //url turim trip id ir employee id
        //get 1api
        //get 2 api
        //get 3 api
    }

    render() {
        const { t } = this.props;
        if (this.props.singleFlightInfo && this.state.carInfo && this.state.apartmentsInfo) {
            return (
                <div>

                    <div className="p-5">
                        <FlightCheckList singleFlightInfo ={this.props.singleFlightInfo} employeeId={this.props.match.params.employeeId} tripId={this.props.match.params.tripId} />
                    </div>
                    <div className="p-5">
                        <CarCheckList carInfo ={this.state.carInfo} employeeId={this.props.match.params.employeeId} tripId={this.props.match.params.tripId} />
                    </div>
                    <div className="p-5">
                        <ApartmentsCheckList apartmentsInfo ={this.state.apartmentsInfo} employeeId={this.props.match.params.employeeId} tripId={this.props.match.params.tripId} />
                    </div>
                </div>
            );
        }
        else {
            return (
                <div>
                    loading
                </div>
            );
        }
    }
}
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        tripbasic: state.tripbasic,
        singleFlightInfo : state.singleFlightInfo
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CheckListDetails));

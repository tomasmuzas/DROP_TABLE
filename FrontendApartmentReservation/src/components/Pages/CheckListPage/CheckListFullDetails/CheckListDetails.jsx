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
            apartmentsInfo: { IsRequired: true, Address: 1234 },
        }
    }

    componentWillMount() {
        this.props.getSingleChecklist(this.props.match.params.employeeId, this.props.match.params.tripId);
    }

    render() {
        const { t } = this.props;
        if (this.props.singleChecklist) {
            return (
                <div>
                    <Link to={'/trip/' + this.props.match.params.tripId }style={{ textDecoration: 'none', color:'black' }}>
                    <button className={`btn btn-lg btn-primary btn-block`}>{t("GoBack")}</button>
                    </Link>
                    <div className="p-5">
                        <FlightCheckList singleFlightInfo ={this.props.singleChecklist.flight} employeeId={this.props.match.params.employeeId} tripId={this.props.match.params.tripId} />
                    </div>
                    <div className="p-5">
                        <CarCheckList carInfo ={this.props.singleChecklist.car} employeeId={this.props.match.params.employeeId} tripId={this.props.match.params.tripId} />
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
        singleChecklist : state.singleChecklist
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CheckListDetails));

import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import CheckListCard from './CheckListCard';

class TripCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checkListInfo: [],
        }
    }


    componentWillMount() {
        var checkListInfos = [
            { firstName: "Martynas", lastName: "Narijauskas", email: "lal@gmail.com", 
            isFlightRequired: false, isApartmentRequired: false, isCarRentRequired: false },
            { firstName: "Martynas", lastName: "Narijauskas", email: "lal@gmail.com", 
            isFlightRequired: false, isApartmentRequired: false, isCarRentRequired: false },
            { firstName: "Martynas", lastName: "Narijauskas", email: "lal@gmail.com", 
            isFlightRequired: false, isApartmentRequired: false, isCarRentRequired: false },
        ];
        this.setState({
            checkListInfo: checkListInfos
        })
    }

    render() {
        return (
            <div>
                <div>
                        {this.state.checkListInfo.map(checkListInfo =><CheckListCard checkListInfo= {checkListInfo} />)}
                </div>
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
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(TripCheckList));

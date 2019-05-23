import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import MyTripCard from './MyTripCard'
import { withTranslation } from "react-i18next";
import { GridLoader } from "react-spinners";

class MyTripsPage extends React.Component {
    componentWillMount() {
        this.props.getMyTrips();
    }

    render() {
        const { t } = this.props; 
        
        if (this.props.myTrips) {
            return (
                <div>
                    <h1 className="row justify-content-md-center mt-5">{t("MyTrips")}</h1>
                    <div>
                        {this.props.myTrips.map(trip =><MyTripCard key={trip.tripId} trip={trip} myId='4779ff4e-fc1d-41d4-8e9a-264ebf3b559b' />)}
                    </div>
                </div>
            );
        }
        else {
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
}
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        myTrips: state.myTrips
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(withTranslation()(MyTripsPage)));

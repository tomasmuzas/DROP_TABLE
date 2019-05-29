import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import MyTripCard from './MyTripCard'
import { withTranslation } from "react-i18next";
import { GridLoader } from "react-spinners";

class MyTripsPage extends React.Component {
    constructor(props){
        super(props);

        this.state = {
            loading: true,
        }
    }


    componentWillMount() {
        this.props.getMyTrips();
        this.setState({
            loading: true
        })
    }

    componentWillReceiveProps(newProps){
        this.setState({
            loading: false
        })
    }

    render() {
        const { t } = this.props;

        if (this.props.myTrips === [] || this.props.myTrips.length === 0 && !this.state.loading) {
            return (<div className="row mt-5">
                <div className="col-12">
                    <h1>{t('EmptyTripsList')}</h1>
                </div>
            </div>
            )
        }
        if (this.props.myTrips.length > 0 ) {
            return (
                <div className="row mt-5">
                    <div className="col-12">
                        <h1 style={{textAlign:'center'}}>{t("MyTrips")}</h1>
                        <div>
                            {this.props.myTrips.map((trip, index) => <MyTripCard key={trip.tripId} index={index} trip={trip}/>)}
                        </div>
                    </div>
                </div>
            );
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
        myTrips: state.myTrips,
        personalChecklist: state.personalChecklist
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(withTranslation()(MyTripsPage)));

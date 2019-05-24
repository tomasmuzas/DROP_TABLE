import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import CheckListCard from './CheckListCard';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import { GridLoader } from "react-spinners";

class TripCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checkListInfo: [],
        }

        this.reserveApartments = this.reserveApartments.bind(this);
    }

    componentWillMount() {
        this.props.clearChecklist();
        this.props.getBasicTrip(this.props.match.params.tripId);
    }

    reserveApartments() {
        this.props.reserveApartmentsForAll(this.props.tripbasic.tripId)
    }

    render() {
        const { t } = this.props;

        if (this.props.tripbasic && this.props.tripbasic.checklistInfos && this.props.tripbasic.tripId === this.props.match.params.tripId) {
            return (
                <div>
                    <h2 className="row mt-5 mx-5">{t("TripDestination")} {this.props.tripbasic.office.address}</h2>
                    <h5 className="row mt-2 mx-5">{t("TripStart")} {new Date(this.props.tripbasic.startTime).toLocaleDateString("lt-LT")} {t("TripEnd")} {new Date(this.props.tripbasic.endTime).toLocaleDateString("lt-LT")}</h5>
                    {
                        // everyone doesnt have apartments requested
                        this.props.tripbasic.checklistInfos.filter(c => !c.isApartmentRequired).length === this.props.tripbasic.checklistInfos.length &&
                        // there are enough available apartments
                        this.props.tripbasic.availableApartments >= this.props.tripbasic.checklistInfos.filter(c => !c.isApartmentRequired).length &&

                        <span className="row mt-5 mx-5" style={{"display": "inline-block"}}> 
                            {t("TripEnoughApartments")} 
                            <Link className={`btn btn-primary mx-3`} to={''} onClick= {this.reserveApartments}>{t("ReserveApartmentForAll")}</Link>
                        </span> 
                    }
                    <div>
                        {this.props.tripbasic.checklistInfos.map((checkListInfo, index) =>
                            <CheckListCard checkListInfo={checkListInfo} index={index} tripId={this.props.tripbasic.tripId} key={checkListInfo.employee.id} />)}
                    </div>
                    <div className="pt-5 justify-content-md-center" style={{display: 'flex',  justifyContent:'center', alignItems:'center'}}>
                    <Link className={`btn btn-lg btn-primary btn-block`} style={{ width: '30%' }} to={''} type="submit">{t("FinishCreation")}</Link>
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
        tripId: state.trips.tripId,
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(TripCheckList));

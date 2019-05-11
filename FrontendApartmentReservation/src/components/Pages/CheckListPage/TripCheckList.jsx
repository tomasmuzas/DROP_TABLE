import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import CheckListCard from './CheckListCard';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

class TripCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checkListInfo: [],
        }
    }

    componentWillMount() {
        this.props.clearFlightInfo();
        this.props.getBasicTrip(this.props.match.params.tripId);
    }

    render() {
        const { t } = this.props;
        if (this.props.tripbasic && this.props.tripbasic.checklistInfos && this.props.tripbasic.tripId === this.props.match.params.tripId) {
            return (
                <div>
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
        tripId: state.trips.tripId,
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(TripCheckList));

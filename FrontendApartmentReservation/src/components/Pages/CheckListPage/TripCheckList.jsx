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
            id: 'eec7080a05c04819b776039cda553d44'
        }
    }


    componentWillMount() {
        this.props.getBasicTrip(this.state.id);
    }

    render() {
        const { t } = this.props;
        if (this.props.tripbasic && this.props.tripbasic.checklistInfos) {
            console.log(this.props.tripbasic.checklistInfos);
            return (
                <div>
                    <div>
                        {this.props.tripbasic.checklistInfos.map(checkListInfo =>
                            <CheckListCard checkListInfo={checkListInfo} tripId={this.props.tripbasic.tripId} key={this.props.tripbasic.tripId} />)}
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
        tripbasic: state.tripbasic
    };
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(TripCheckList));

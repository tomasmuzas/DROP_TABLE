import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux'; import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';

class CarCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            carInfo: { IsRequired: true, CarNumber: 12342 },
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleCarNumberChange = this.handleCarNumberChange.bind(this);
    }

    componentWillMount() {
        //this.props.employeeId;
        //this.props.tripId;
        //getchecklistinfo
    }

    handleSubmit(e) {
        e.preventDefault();
        const { carInfo } = this.state;
    }

    handleIsRequiredChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.IsRequired = e.target.checked;
        this.setState({
            carInfo: carInfo
        });
    }

    handleCarNumberChange(e) {
        var carInfo = this.state.carInfo;
        carInfo.CarNumber = e.target.value;
        this.setState({
            carInfo: carInfo
        });
    }

    render() {

        const { t } = this.props;
        if (this.state.carInfo) {
            return (
                <div className=" jumbotron mx-auto col-12 col-lg-6 pb-0 mt-0 pt-0 px-0">
                    <div style={{ borderBottom: '1px solid #b4bac4' }}>
                        <label className='pt-2 pl-2'>{t("CarRequired")}</label>
                        <Checkbox style={{ float: 'right' }} checked={this.state.carInfo.IsRequired} onChange={this.handleIsRequiredChange} />
                    </div>

                    <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 mt-0 pt-0`} hidden={!this.state.carInfo.IsRequired}>
                        <form className={`form-signin`} onSubmit={this.handleSubmit} >
                            <div className="form-group mb-2">
                                {t("CarNumber")}
                                <input type="text" id="carNumber" className={`form-control`} placeholder={t("carNumber")}
                                    required autoFocus name="carNumber" value={this.state.carInfo.CarNumber}
                                    onChange={this.handleCarNumberChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveCarInfo")}</button>
                        </form>
                    </div>
                </div>
            )
        }
        else {
            return (
                <div>
                    loading
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
        user: state.user
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CarCheckList));
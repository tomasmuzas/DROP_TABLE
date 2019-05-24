import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import * as actionCreators from '../../../../actions';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux'; import { Link } from 'react-router-dom';
import Checkbox from '@material-ui/core/Checkbox';
import { GridLoader } from 'react-spinners';

class ApartmentsCheckList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            apartmentsInfo: { IsRequired: true, Address: 1234 }
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleIsRequiredChange = this.handleIsRequiredChange.bind(this);
        this.handleApartmentAddressChange = this.handleApartmentAddressChange.bind(this);
    }

    componentWillMount() {
        //this.props.employeeId;
        //this.props.tripId;
        //getchecklistinfo
    }

    handleSubmit(e) {
        e.preventDefault();
        const { apartmentsInfo } = this.state;
    }

    handleIsRequiredChange(e) {
        var aparmentsInfo = this.state.apartmentsInfo;
        aparmentsInfo.IsRequired = e.target.checked;
        this.setState({
            aparmentsInfo: aparmentsInfo
        });
    }

    handleApartmentAddressChange(e) {
        var aparmentsInfo = this.state.apartmentsInfo;
        aparmentsInfo.Address = e.target.value;
        this.setState({
            aparmentsInfo: aparmentsInfo
        });
    }

    render() {

        const { t } = this.props;
        if (this.state.apartmentsInfo) {
            return (
                <div className=" jumbotron mx-auto col-12 col-lg-6 pb-0 mt-0 pt-0 px-0">
                    <div style={{ borderBottom: '1px solid #b4bac4' }}>
                        <label className='pt-2 pl-2'>{t("ApartmentRequired")}</label>
                        <Checkbox style={{ float: 'right' }} checked={this.state.apartmentsInfo.IsRequired} onChange={this.handleIsRequiredChange} />
                    </div>

                    <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 mt-0 pt-0`} hidden={!this.state.apartmentsInfo.IsRequired}>
                        <form className={`form-signin`} onSubmit={this.handleSubmit} >
                            <div className="form-group mb-2">
                                {t("ApartmentsAddres")}
                                <input type="text" id="ApartmentsAddres" className={`form-control`} placeholder={t("FlightNumber")}
                                    required autoFocus name="ApartmentsAddres" value={this.state.apartmentsInfo.Address}
                                    onChange={this.handleApartmentAddressChange} />
                            </div>
                            <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SaveApartmentsInfo")}</button>
                        </form>
                    </div>
                </div>
            )
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
        user: state.user
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(ApartmentsCheckList));
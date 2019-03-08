import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class ApartmentsPage extends React.Component {

    componentWillMount() {
        this.props.getAllApartments();
    }

    render() {
        if (this.props.apartments) {
            return (
                <div>
                    This is apartments page
                    {this.props.apartments.map(apartment => <div> {apartment} </div>)}
                </div>
            );
        }
        else{
            return(
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
        apartments: state.apartments
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(ApartmentsPage));

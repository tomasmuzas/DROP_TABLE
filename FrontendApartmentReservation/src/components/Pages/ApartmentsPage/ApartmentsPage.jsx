import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import { GridLoader } from "react-spinners";

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
        apartments: state.apartments
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(ApartmentsPage));

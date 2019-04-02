import React from 'react';
import { render } from 'react-dom';
import Application from './Application';
import { I18nextProvider } from "react-i18next";
import i18n from "./i18n";

import './styles.css';
import 'bootstrap/dist/css/bootstrap.css';

render(
    <I18nextProvider i18n={i18n}>
    <Application />
    </I18nextProvider>,
    document.getElementById('root'));
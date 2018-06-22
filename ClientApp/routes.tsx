import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { NewsProvidersList } from './components/NewsProvidersList';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/providers' component={NewsProvidersList} />
    <Route path='/fetchdata' component={ FetchData } />
</Layout>;

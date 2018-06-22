import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { NewsFeedItem } from './NewsFeedItem';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {

    state = { feedItmes: [] };
    componentDidMount() {

        this.getAllFeeds();
    }

    getAllFeeds = () => {

        fetch('api/News/Get?')
            .then(response => response.json() as Promise<any[]>)
            .then(data => {
                this.setState({ feedItmes: data })
            });

    }
    public render() {
        console.log(this.state.feedItmes);
        return <div>
            {
                this.state.feedItmes ? this.state.feedItmes.map((feedItem, index) => (
                    < NewsFeedItem data={feedItem} key={index} {...({} as RouteComponentProps<any>)} />
                )) : null

            }

        </div>;
    }
}

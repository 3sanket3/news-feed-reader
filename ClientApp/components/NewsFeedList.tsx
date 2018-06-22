import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { NewsFeedItem } from './NewsFeedItem';

export class NewsFeedList extends React.Component<any & RouteComponentProps<{}>, {}> {

    
    static PropTypes ={
        onlySubscribed: React.PropTypes.bool,
        providerId: React.PropTypes.number,
        renderIfNoItemsFound: React.PropTypes.func
    }

    state = { feedItmes: [], apiResponded : false /*renderIfNoItemsFound should not be called if api is yet to respond */ };
    
    componentDidMount() {
        console.log("onlySubscribed "+this.props.onlySubscribed);
        this.getAllFeeds();
    }

    getAllFeeds = () => {

        let url= this.props.onlySubscribed? 'api/News/Get?onlySubscribed=true' : 'api/News/Get';    

        if(this.props.providerId>0){
            url = 'api/News/Get?providerId='+this.props.providerId;
        }else if (this.props.onlySubscribed) {
            url = 'api/News/Get?onlySubscribed=true';
        }else {
            url ='api/News/Get';
        }
        
       
        fetch(url)
            .then(response => response.json() as Promise<any[]>)
            .then(data => {
                this.setState({ feedItmes: data, apiResponded : true })
            });
        
    }
    public render() {
        console.log(this.state.feedItmes);
        return <div>
            {
               this.state.feedItmes && this.state.feedItmes.length ? this.state.feedItmes.map( (feedItem, index) => (
                     < NewsFeedItem data={feedItem} key={index} {...({} as RouteComponentProps<any>)} />
                )) : this.state.apiResponded && this.props.renderIfNoItemsFound && this.props.renderIfNoItemsFound()
            
            }
            
        </div>;
    }
}

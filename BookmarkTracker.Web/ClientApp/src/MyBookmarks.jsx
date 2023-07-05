import React, { useState, useEffect } from "react";
import axios from "axios";
import { useAuth } from "./AuthContext";
import { Link } from "react-router-dom";
import BookmarkRow from "./BookmarkRow";


const MyBookmarks = () => {

    const [myBookmarks, setMyBookmarks] = useState([]);
    const { user } = useAuth();

    const getMyBookmarks = async () => {

        const { data } = await axios.get('/api/user/getbookmarks');
        setMyBookmarks(data);
        console.log(data);
    }
    useEffect(() => {

        getMyBookmarks();

    }, []);


    const onUpdateClick = async (bookmarkTitle, id) => {

        await axios.post('/api/user/updatebookmarktitle', { title: bookmarkTitle, id });
        getMyBookmarks();

    }


    const onDeleteClick = async (id) => {
        await axios.post('/api/user/deletebookmark', {id} );
        getMyBookmarks();
    }

    return (
        <div style={{ marginTop: 20 }}>
            <div className="row">
                <div className="col-md-12">
                    <h1>Welcome back {user.firstName} {user.lastName} </h1>
                    <Link to="/addbookmark" className="btn btn-primary btn-block">
                        Add Bookmark
                    </Link>
                </div>
            </div>
            <div className="row" style={{ marginTop: 20 }}>
                <table className="table table-hover table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Url</th>
                            <th>Edit/Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        {myBookmarks.length > 0 &&
                            myBookmarks.map((b) =>
                                <BookmarkRow
                                    key={b.id}
                                    bookmark={b}
                                    onUpdateClick={(bookmarkTitle) => onUpdateClick(bookmarkTitle, b.id)}
                                    onDeleteClick={() => onDeleteClick(b.id)}
                                />
                            )}
                    </tbody>
                </table>
            </div>
        </div>

    )
}

export default MyBookmarks;
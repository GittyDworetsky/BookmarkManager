import React, { useState, useEffect } from "react";
import axios from "axios";
import { useAuth } from "./AuthContext";
import { Link } from "react-router-dom";
import BookmarkRow from "./BookmarkRow";


const MyBookmarks = () => {

    const [myBookmarks, setMyBookmarks] = useState([]);
    const [inEditIds, setInEditIds] = useState([]);
    const [bookmarkName, setBookmarkName] = useState();
    const { user } = useAuth();

    const getMyBookmarks = async () => {

        const { data } = await axios.get('/api/user/getbookmarks');
        setMyBookmarks(data);
        console.log(data);
    }
    useEffect(() => {

        getMyBookmarks();

    }, []);

    const onTextChange = (e) => {
        setBookmarkName(e.target.value);
    }

    const onEditClick = (id) => {

        setInEditIds([...inEditIds, id]);
    }

    const onUpdateClick = async (id) => {

        await axios.post('/api/user/updatebookmarktitle', { title: bookmarkName, id });
        getMyBookmarks();
        const copy = inEditIds.filter(i => i !== id);
        setInEditIds(copy);       
        setBookmarkName('');

    }

    const onCancelClick = (id) => {

        const copy = inEditIds.filter(i => i !== id);
        setInEditIds(copy); 
        setBookmarkName('');
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
                                    bookmarkName={bookmarkName}
                                    editMode={inEditIds.includes(b.id)}
                                    onEditClick={() => onEditClick(b.id)}
                                    onCancelClick={() => onCancelClick(b.id)}
                                    onUpdateClick={() => onUpdateClick(b.id)}
                                    onDeleteClick={() => onDeleteClick(b.id)}
                                    onTextChange={onTextChange}
                                />
                            )}
                    </tbody>
                </table>
            </div>
        </div>

    )
}

export default MyBookmarks;
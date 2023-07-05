import React, { useState } from "react";


const BookmarkRow = (props) => {

    const { bookmark, onUpdateClick, onDeleteClick } = props;
    const [editMode, setEditMode] = useState(false);
    const [bookmarkTitle, setBookmarkTitle] = useState(bookmark.title);

    const onTextChange = (e) => {
        setBookmarkTitle(e.target.value);
    }

    const onEditClick = (id) => {
        setEditMode(true);
    }

    const onCancelClick = () => {
        setBookmarkTitle(bookmark.title);
        setEditMode(false);
    }

    const myUpdateClick = () => {
        onUpdateClick(bookmarkTitle)
        setEditMode(false);
    }

    return (
        <tr key={bookmark.id}>
            <td>{!editMode ? bookmark.title : <input type="text" value={bookmarkTitle} onChange={onTextChange} />}</td>
            <td>
                <a href={bookmark.url} target="_blank">
                    {bookmark.url}
                </a>
            </td>
            <td>
                {!editMode ? (
                    <button onClick={onEditClick} className="btn btn-success">
                        Edit Title
                    </button>
                ) : (
                    <>
                        <button onClick={myUpdateClick} className="btn btn-warning">Update</button>
                        <button onClick={onCancelClick} className="btn btn-info">Cancel</button>
                    </>
                )}
                <button onClick={onDeleteClick} className="btn btn-danger" style={{ marginLeft: 10 }}>
                    Delete
                </button>
            </td>
        </tr>
    )


}

export default BookmarkRow;
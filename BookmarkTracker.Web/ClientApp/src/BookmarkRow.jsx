import React from "react";

const BookmarkRow = (props) => {
    const { bookmark, bookmarkName, onEditClick, onUpdateClick, onCancelClick, onDeleteClick, editMode, onTextChange } = props;
    return (
        <tr key={bookmark.id}>
            <td>{!editMode ? bookmark.title : <input type="text" value={bookmarkName} onChange={onTextChange} />}</td>
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
                        <button onClick={onUpdateClick} className="btn btn-warning">Update</button>
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
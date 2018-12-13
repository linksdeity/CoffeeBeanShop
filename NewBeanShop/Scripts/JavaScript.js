function DeleteChoice(ItemID) {
    if (confirm('Are you sure you want to delete???')) {
        window.location.href = "/DataBase/DeleteBean/" + ItemID;
    } else {
        return false;
    }
}
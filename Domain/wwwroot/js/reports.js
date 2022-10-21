$(document).ready(function () {
  const year = new Date().getFullYear();
  const month = new Date().getMonth() + 1;
  const day =new Date().getDate();
  $('#fromDate').val(`${year}-${month}-01`);
  $('#toDate').val(`${year}-${month}-${day}`);
});
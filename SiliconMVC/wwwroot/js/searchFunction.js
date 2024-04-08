function searchQuery() {
    try {
        document.querySelector('#search').addEventListener('keyup', function () {
            updateCourseByFilter()
        })
    }
    catch { }
}

function updateCourseByFilter() {
    const category = document.querySelector('#search').value

    const url = '/api/Courses/GetBySearch?search={search}?key={apiKey}"'

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.items').innerHTML = dom.querySelector('.items').innerHTML
        })
}
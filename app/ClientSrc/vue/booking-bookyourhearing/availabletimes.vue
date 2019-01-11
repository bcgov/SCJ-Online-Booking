<template>
    <div class="row">
        <div class="col-md-12" style="height:100px;">
            <div>
                <a @click="toSlide(0)">To Slide 1</a>
                <a @click="toSlide(3)">To Slide 4</a>
                <a @click="toSlide(7)">To Slide 8</a>
            </div>
            <swiper :options="swiperOption" ref="mySwiper" id="swipe-container">
                <div class="swiper-pagination" slot="pagination"></div>
                <div class="swiper-button-prev" slot="button-prev"></div>
                <div class="swiper-button-next" slot="button-next"></div>
            </swiper>
        </div>
    </div>
</template>

<style>
    .custom-slide-header{
        margin-bottom: 10px;
        font-weight: bold;
    }

    .custom-slide-times{
        border:solid 1px #535353;
        height: 150px;
        padding: 5px;
    }

    .custom-slide-time {
        background-color: aliceblue;
        padding: 5px;
        margin:5px;
        cursor: pointer;
    }
</style>
<script>
    import Vue from "vue";
    import VueAwesomeSwiper from 'vue-awesome-swiper';
    import { swiper, swiperSlide } from 'vue-awesome-swiper';

    Vue.use(VueAwesomeSwiper);

    export default {
        components: {
            swiper,
            swiperSlide
        },
        data() {
            return {
                swiperOption: {
                    slidesPerView: 5,
                    centeredSlides: true,
                    spaceBetween: 30,
                    pagination: {
                        el: '.swiper-pagination',
                        type: 'fraction'
                    },
                    navigation: {
                        nextEl: '.swiper-button-next',
                        prevEl: '.swiper-button-prev'
                    },
                    breakpoints: {
                        1024: {
                            slidesPerView: 4,
                            spaceBetween: 40
                        },
                        768: {
                            slidesPerView: 3,
                            spaceBetween: 30
                        },
                        640: {
                            slidesPerView: 2,
                            spaceBetween: 20
                        },
                        320: {
                            slidesPerView: 1,
                            spaceBetween: 10
                        }
                    },
                    virtual: {
                        slides: (function () {
                            const slides = [];

                            //TMP
                            //This is where the rest API will be called to fetch the data
                            var serverData = new Array();
                            serverData.push({ 'Day': 'Wednesday', 'Date': 'January 2, 2019', 'Times': [ '12:00 - 12:45', '13:00 - 14:00', '15:00 - 15:30' ] });
                            serverData.push({ 'Day': 'Thursday', 'Date': 'January 3, 2019', 'Times': [ '10:00 - 11:00'] });
                            serverData.push({ 'Day': 'Friday', 'Date': 'January 4, 2019', 'Times': [ '8:00 - 8:30', '13:00 - 13:30' ] });
                            serverData.push({ 'Day': 'Saturday', 'Date': 'January 5, 2019', 'Times': [ '16:00 - 16: 30', '17:00 - 17:30', '17:30 - 18:00' ] });
                            serverData.push({ 'Day': 'Sunday', 'Date': 'January 6, 2019', 'Times': [ '13:00 - 13:45', '14:00 - 15:00', '16:00 - 17:30' ] });
                            serverData.push({ 'Day': 'Monday', 'Date': 'January 7, 2019', 'Times': [ '11:00 - 12:00' ] });
                            serverData.push({ 'Day': 'Tuesday', 'Date': 'January 8, 2019', 'Times': [ '9:00 - 10:30', '14:00 - 14:30' ] });
                            serverData.push({ 'Day': 'Wednesday', 'Date': 'January 9, 2019', 'Times': [ '17:00 - 17: 30', '18:00 - 18:30', '18:30 - 19:00' ] });

                            for (let i = 0; i < serverData.length; i++) {
                                slides.push(formatData(serverData[i]))
                            }
                            return slides
                        }())
                    }
                }
            }
        },
        methods: {
            toSlide(i) {
                this.$refs.mySwiper.swiper.slideTo(i, 0)
            }
        }
    }


    //SLIDER TEMPLATES

    //Main slider template
    var slideTemplate =
        '<div class="custom-slide-container">' +
            '<div class="custom-slide-header">' +
                '##DAY##' +
                '<br />' +
                '##DATE##' +
            '</div>' +
            '<div class="custom-slide-times">' +
               '##TIMES##' +
            '</div>' +
        '</div>';

    //Template for each record in the main slider
    var slideTimeTemplate =
        '<div class="custom-slide-time">' +
            '##TIME##' +
        '</div>';


    //HELPER FUNCTIONS

    //Populate the data in the templates
    function formatData(data) {

        //reference to the slider main template layout
        var tmp = slideTemplate;

        //reference to the slider time item in the main slider
        var tmpTime = slideTimeTemplate;

        //generated html that will be used to display in the slider
        var tmpTimes = '';

        //set title
        tmp = tmp.replace('##DAY##', data.Day);
        tmp = tmp.replace('##DATE##', data.Date);

        //loop through times and add
        for (var i = 0; i < data.Times.length; i++) {
            tmpTimes += tmpTime.replace('##TIME##', data.Times[i]);
        }

        //set times
        tmp = tmp.replace('##TIMES##', tmpTimes);

        //return html
        return tmp;
    }
</script>

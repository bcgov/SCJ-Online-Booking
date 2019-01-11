<template>
    <div class="row">
        <div class="col-md-12" style="height:100px;">
            <div>
                <a @click="toSlide(0)">To Slide 1</a>
                <a @click="toSlide(249)">To Slide 250</a>
                <a @click="toSlide(499)">To Slide 500</a>
            </div>
            <swiper :options="swiperOption" ref="mySwiper">
                <div class="swiper-pagination" slot="pagination"></div>
                <div class="swiper-button-prev" slot="button-prev"></div>
                <div class="swiper-button-next" slot="button-next"></div>
            </swiper>
        </div>
    </div>
</template>

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

                            for (let i = 0; i < 600; i += 1) {
                                slides.push('Slide ' + (i + 1))
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

    function addDate(dateObj) {

    }

    function addTime(time) {

    }
</script>

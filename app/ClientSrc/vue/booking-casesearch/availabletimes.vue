<template>
    <div class="row">
        <div class="col-md-12" style="height:100px;">
            <swiper ref="mySwiper" :options="swiperOption" id="swipe-container">
                <swiper-slide v-for="entry in availabletimes">
                    <div class="custom-slide-container">
                        <div class="custom-slide-header">
                            {{ entry.weekday }}
                            <br />
                            {{ entry.formattedDate }}
                        </div>
                        <div class="custom-slide-times">
                            <div class="custom-slide-time" v-for="container in entry.times" @click="selectTime(container.containerId, container.startDateTime)"
                                 :class="{'selected': container.containerId === selectedContainerId}">
                                {{ container.start }} - {{ container.end }}
                            </div>
                        </div>
                    </div>
                </swiper-slide>
                <div class="swiper-button-prev" slot="button-prev"></div>
                <div class="swiper-button-next" slot="button-next"></div>
            </swiper>
        </div>
    </div>
</template>

<style lang="scss">
    .custom-slide-header{
        margin-bottom: 10px;
        font-weight: bold;
        text-align: center;
    }

    .custom-slide-times {
        border: 1px solid rgba(0, 0, 0, 0.125);
        height: 150px;
        padding: 5px;
    }

    .custom-slide-time {
        background-color: rgba(0, 0, 0, 0.10);
        padding: 5px;
        margin: 5px;
        cursor: pointer;
        text-align: center;

        &.selected {
            background-color: blue;
            color: white;
        }    
    }

    .swiper-container
    {
        margin-top: -15px !important;
    }
</style>

<script>
    import Vue from "vue";
    import VueAwesomeSwiper from 'vue-awesome-swiper';
    import { swiper, swiperSlide } from 'vue-awesome-swiper';
    import axios from 'axios';

    Vue.use(VueAwesomeSwiper);

    export default {
        components: {
            swiper,
            swiperSlide
        },
        props: {
            locationId: Number,
            hearingType: Number
        },
        data() {
            return {
                availabletimes: [],
                selectedContainerId: null,
                selectedBookingTime: null,
                swiperOption: {
                    slidesPerView: 5,
                    centeredSlides: false,
                    spaceBetween: 20,
                    pagination: {
                        el: '.swiper-pagination',
                        clickable: true
                    },
                    navigation: {
                        nextEl: '.swiper-button-next',
                        prevEl: '.swiper-button-prev'
                    },
                    breakpoints: {
                        1200: {
                            slidesPerView: 4,
                            spaceBetween: 18
                        },
                        1000: {
                            slidesPerView: 3,
                            spaceBetween: 16
                        },
                        780: {
                            slidesPerView: 2,
                            spaceBetween: 14
                        },
                        420: {
                            slidesPerView: 1,
                            spaceBetween: 12
                        }
                    }
                }
            }
        },
        methods: {
            selectTime(containerId, bookingTime) {
                this.selectedContainerId = containerId;
                this.selectedBookingTime = bookingTime;
                $('#ContainerId').val(containerId);
                $('#BookingTime').val(bookingTime);
            }
        },
        created() {
            let self = this;
            axios.get(`/booking/api/available-dates-by-location/${this.locationId}/${this.hearingType}`)
                .then(response => {
                    self.availabletimes = response.data;
                });
        }
    }
</script>
